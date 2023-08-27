using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using User_Management_Service.Context;
using User_Management_Service.Repository.IRepository;

namespace User_Management_Service.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private vsm_db_userContext _db;
        public RoleRepository(vsm_db_userContext db)
        {
            _db = db;
        }
        public async Task Create(Role model)
        {
            try
            {
                model.IsDelete = false;
                await _db.Roles.AddAsync(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var model = await _db.Roles.Where(ss => ss.RoleId == id && !ss.IsDelete).FirstOrDefaultAsync();

                if (model != null)
                {
                    model.IsDelete = true;
                    _db.Roles.Update(model);

                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Role>> GetList()
        {
            var value = new List<Role>();
            try
            {
                value = await _db.Roles.Where(ss => ss.IsDelete == false).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }

        public async Task<Role> GetRole(Expression<Func<Role, bool>> filter = null)
        {
            IQueryable<Role> query = _db.Roles;
            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Role> Update(Role model)
        {
            try
            {
                model.IsDelete = false;
                _db.Roles.Update(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
    }
}
