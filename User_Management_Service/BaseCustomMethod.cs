using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User_Management_Service.Models.DTO;

namespace User_Management_Service
{
    public class BaseCustomMethod
    {
        private const string keyHash = "Custom_Hash_123";
        private const string _keyEnDes = "FVwXlmsvlIMSYGnzn23PHjZBRBpehN1I"; 
        public string HashPassword(string password)
        {
            // Combine the password with the additional key
            string passwordWithKey = password + keyHash;

            // Generate a random salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the combined password and salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordWithKey, salt);

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Combine the password with the additional key
            string passwordWithKey = password + keyHash;

            // Verify the combined password against the hashed password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(passwordWithKey, hashedPassword);

            return isPasswordValid;
        }


        public string GenerateJwtToken(UserDTO user,int expiresInMinutes = 60)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyEnDes));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName),
            //new Claim(ClaimTypes.Role, user.RoleList),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
