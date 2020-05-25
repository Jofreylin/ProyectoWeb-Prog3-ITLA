using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Service
{
    public interface IUserPosterService
    {
        IEnumerable<UserPoster> GetAll();
        UserPoster Get(int id);
        bool Add(UserPoster model);
        bool Update(UserPoster model);
        bool Delete(int id);
    }
    public class UserPosterService : IUserPosterService
    {
        private readonly ProjectDbContext _projectDbContext;

        public UserPosterService(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public bool Add(UserPoster model)
        {
            try
            {
                _projectDbContext.Add(model);
                _projectDbContext.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                _projectDbContext.Entry(new UserPoster { Id = id }).State = EntityState.Deleted;
                _projectDbContext.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }

            return false;
        }

        public UserPoster Get(int id)
        {
            var result = new UserPoster();
            try
            {
                result = _projectDbContext.UserPoster.Single(x => x.Id == id);
            }catch(Exception e)
            {
                
            }
            return result;
        }

        public IEnumerable<UserPoster> GetAll()
        {
            var result = new List<UserPoster>();
            try
            {
                result = _projectDbContext.UserPoster.ToList();
            }catch(Exception e)
            {

            }
            return result;
        }

        public bool Update(UserPoster model)
        {
            try
            {
                var originalModel = _projectDbContext.UserPoster.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Contra = model.Contra;
                originalModel.Email = model.Email;
                originalModel.NombreCalle = model.NombreCalle;
                originalModel.NombreCiudad = model.NombreCiudad;
                originalModel.NombreEmpresa = model.NombreEmpresa;
                originalModel.NombrePais = model.NombrePais;

                _projectDbContext.Update(originalModel);
                _projectDbContext.SaveChanges();

            }catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
