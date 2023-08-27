namespace User_Management_Service.Models.DTO
{
    public class UserUpdateDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; } 
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
