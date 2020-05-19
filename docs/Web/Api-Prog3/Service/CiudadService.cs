using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Service
{
    
    public interface ICiudadService
    {
        IEnumerable<Ciudad> GetAll();
        Ciudad Get(int id);
        bool Add(Ciudad model);
        bool Update(Ciudad model);
        bool Delete(int id);
    }
    public class CiudadService : ICiudadService
    {
        private readonly ProjectDbContext _ProjectDbContext;

        public CiudadService(ProjectDbContext projectDbContext)
        {
            _ProjectDbContext = projectDbContext;
        }

        public bool Add(Ciudad model)
        {
            try
            {
                _ProjectDbContext.Add(model);
                _ProjectDbContext.SaveChanges();
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
                _ProjectDbContext.Entry(new Ciudad { Id = id }).State = EntityState.Deleted;
                _ProjectDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Ciudad Get(int id)
        {
            var result = new Ciudad();

            try
            {

                result = _ProjectDbContext.Ciudad.Single(x => x.Id == id);


            }catch(Exception e)
            {

            }

            return result;
        }

        public IEnumerable<Ciudad> GetAll()
        {
            var result = new List<Ciudad>();

            try
            {

                result = _ProjectDbContext.Ciudad.ToList();

            }catch(Exception e){

            }

            return result;
        }

        public bool Update(Ciudad model)
        {
            try
            {

                var originalModel = _ProjectDbContext.Ciudad.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Nombre = model.Nombre;
                originalModel.NombrePais = model.NombrePais;
                originalModel.NombrePaisNavigation = model.NombrePaisNavigation;
                originalModel.Post = model.Post;
                originalModel.UserPoster = model.UserPoster;

                _ProjectDbContext.Update(originalModel);
                _ProjectDbContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
