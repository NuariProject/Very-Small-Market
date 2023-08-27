using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using User_Management_Service.Context;
using User_Management_Service.Models.DTO;
using User_Management_Service.Repository.IRepository;

namespace User_Management_Service.Repository
{
    public class UserRepository :IUserRepository
    {
        private vsm_db_userContext _db;
        private readonly IMapper _mapper;
        private readonly BaseCustomMethod _baseMethod;
        public UserRepository(vsm_db_userContext db, IMapper mapper, BaseCustomMethod baseMethod)
        {
            _mapper = mapper;
            _db = db;
            _baseMethod = baseMethod;
        }

        public async Task<List<UserDTO>> GetList()
        {
            var userList = new List<UserDTO>();
            var roleList = new List<RoleDTO>();
            try
            {
                var users = await _db.Users.Where(ss => ss.IsDelete == false).ToListAsync();
                var roles = await _db.Roles.ToListAsync();

                userList = _mapper.Map<List<UserDTO>>(users);
                roleList = _mapper.Map<List<RoleDTO>>(roles);

                foreach (var item in userList)
                {
                    item.Role = roleList.Where(ss => ss.RoleId == item.RoleId).FirstOrDefault();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return userList;
        }

        public async Task<UserDTO> GetUser(Expression<Func<User, bool>> filter = null)
        {
            var userList = new UserDTO();
            var roleList = new List<RoleDTO>();
            IQueryable<User> query = _db.Users;

            try
            {
                if (filter != null)
                {
                    var user = await query.Where(filter).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        var roles = await _db.Roles.ToListAsync();

                        userList = _mapper.Map<UserDTO>(user);
                        roleList = _mapper.Map<List<RoleDTO>>(roles);

                        userList.Role = roleList.Where(ss => ss.RoleId == userList.RoleId).FirstOrDefault();
                    }
                    else
                    {
                        userList = null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return userList;
        }

        public async Task Create(UserDTO model)
        {
            using (var trx = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = _mapper.Map<User>(model);

                    user.Password = _baseMethod.HashPassword(model.Password);
                    user.CreatedDate = DateTime.Now;
                    user.IsDelete = false;

                    await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();

                    trx.Commit();

                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        public async Task<UserDTO> Update(UserDTO model)
        {
            using (var trx = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = _db.Users.Where(ss => ss.UserId == model.UserId && !ss.IsDelete).FirstOrDefault();

                    user.RoleId = model.RoleId;
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Password = _baseMethod.HashPassword(model.Password);
                    user.ModifiedDate = DateTime.Now;
                    user.IsDelete = false;

                    _db.Users.Update(user);
                    await _db.SaveChangesAsync();

                    trx.Commit();

                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }

            return model;
        }

        public async Task Delete(int id)
        {
            try
            {
                var model = await _db.Users.Where(ss => ss.UserId == id && !ss.IsDelete).FirstOrDefaultAsync();

                if (model != null)
                {
                    model.ModifiedDate = DateTime.Now;
                    model.IsDelete = true;
                    _db.Users.Update(model);

                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
