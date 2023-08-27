using Microsoft.EntityFrameworkCore;
using User_Management_Service.Context;
using User_Management_Service.Models.DTO;
using User_Management_Service.Repository.IRepository;

namespace User_Management_Service.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private BaseCustomMethod _method;
        private vsm_db_userContext _db;
        public AuthRepository(vsm_db_userContext db, BaseCustomMethod method)
        {
            _db = db;
            _method = method;

        }
        public async Task<string> Authentication(AuthDTO model, UserDTO user)
        {
            try
            {
                if(!_method.VerifyPassword(model.password, user.Password)) 
                {
                    return "0";
                }

                using (var trx = await _db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var token = await _db.Tokens.Where(ss => ss.UserId == user.UserId).FirstOrDefaultAsync();

                        if (token == null)
                        {
                            var create = new Token();
                            create.UserId = user.UserId;
                            create.TokenValue = _method.GenerateJwtToken(user);
                            create.ExpiredTime = DateTime.Now.AddMinutes(60);

                            await _db.Tokens.AddAsync(create);
                            await _db.SaveChangesAsync();

                            trx.Commit();

                            return create.TokenValue;
                        }
                        else
                        {
                            token.TokenValue = _method.GenerateJwtToken(user);
                            token.ExpiredTime = DateTime.Now.AddMinutes(60);

                            _db.Tokens.Update(token);
                            await _db.SaveChangesAsync();

                            trx.Commit();

                            return token.TokenValue;
                        }
                    }
                    catch (Exception)
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
