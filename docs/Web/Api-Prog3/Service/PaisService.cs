using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Service
{
    public interface IPaisService
    {
        IEnumerable<Pais> GetAll();
        Pais Get(int id);
        bool Delete(int id);
        bool Update(Pais model);
        bool Add(Pais model);

    }
    public class PaisService : IPaisService
    {
        private readonly ProjectDbContext _ProjectDbContext;

        public PaisService(ProjectDbContext projectDbContext)
        {
            _ProjectDbContext = projectDbContext;
        }

        public bool Add(Pais model)
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
                _ProjectDbContext.Entry(new Pais { Id = id }).State = EntityState.Deleted;
                _ProjectDbContext.SaveChanges();

            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public Pais Get(int id)
        {
            var result = new Pais();

            try
            {
                result = _ProjectDbContext.Pais.Single(x => x.Id == id);


            }catch(Exception e)
            {

            }

            return result;
        }

        public IEnumerable<Pais> GetAll()
        {
            var result = new List<Pais>();
            try
            {
                result = _ProjectDbContext.Pais.ToList();


            }catch(Exception e)
            {

            }

            return result;
        }

        public bool Update(Pais model)
        {
            try
            {
                var originalModel = _ProjectDbContext.Pais.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Nombre = model.Nombre;

                _ProjectDbContext.Update(originalModel);
                _ProjectDbContext.SaveChanges();

            }catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
