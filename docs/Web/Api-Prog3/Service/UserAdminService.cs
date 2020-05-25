using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Service
{
    public interface IUserAdminService
    {
        IEnumerable<UserAdmin> GetAll();
        UserAdmin Get(int id);
        bool Add(UserAdmin model);
        bool Delete(int id);
        bool Update(UserAdmin model);
    }
    public class UserAdminService : IUserAdminService
    {
        private readonly ProjectDbContext _projectDbContext;

        public UserAdminService(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public bool Add(UserAdmin model)
        {
            try
            {
                _projectDbContext.Add(model);
                _projectDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                _projectDbContext.Entry(new UserAdmin { Id = id }).State = EntityState.Deleted;
                _projectDbContext.SaveChanges();

            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public UserAdmin Get(int id)
        {
            var result = new UserAdmin();
            try
            {
                result = _projectDbContext.UserAdmin.Single(x => x.Id == id);

            }catch(Exception e)
            {

            }
            return result;
        }

        public IEnumerable<UserAdmin> GetAll()
        {
            var result = new List<UserAdmin>();
            try
            {
                result = _projectDbContext.UserAdmin.ToList();
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public bool Update(UserAdmin model)
        {
            try
            {
                var originalModel = _projectDbContext.UserAdmin.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Nombre = model.Nombre;
                originalModel.Usuario = model.Usuario;
                originalModel.Email = model.Email;
                originalModel.Contraseña = model.Contraseña;
                

                _projectDbContext.Update(originalModel);
                _projectDbContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
