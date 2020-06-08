using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetAll();
        Categoria Get(int id);
        bool Delete(int id);
        bool Update(Categoria model);
        bool Add(Categoria model);

    }
    public class CategoriaService : ICategoriaService
    {
        private readonly ProjectDbContext _ProjectDbContext;
        public CategoriaService(ProjectDbContext projectDbContext)
        {
            _ProjectDbContext = projectDbContext;
        }

        public IEnumerable<Categoria> GetAll()
        {
            var result = new List<Categoria>();
            try
            {
                result = _ProjectDbContext.Categoria.ToList();
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public Categoria Get(int id)
        {
            var result = new Categoria();

            try
            {
                result = _ProjectDbContext.Categoria.Single(x => x.Id == id);
            }
            catch (Exception e)
            {

            }

            return result;
        }

        public bool Add(Categoria model)
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

        public bool Update(Categoria model)
        {
            try
            {
                var originalModel = _ProjectDbContext.Categoria.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Nombre = model.Nombre;
                originalModel.Logo = model.Logo;

                _ProjectDbContext.Update(originalModel);
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
                _ProjectDbContext.Entry(new Categoria { Id = id }).State = EntityState.Deleted;
                _ProjectDbContext.SaveChanges();

            }catch(Exception e)
            {
                return false;

            }

            return true;
        }

        

        

        
    }
}
