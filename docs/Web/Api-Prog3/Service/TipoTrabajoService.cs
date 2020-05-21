using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Service
{
    public interface ITipoTrabajoService
    {
        IEnumerable<TipoTrabajo> GetAll();
        TipoTrabajo Get(int id);
        bool Add(TipoTrabajo model);
        bool Update(TipoTrabajo model);
        bool Delete(int id);

    }
    public class TipoTrabajoService : ITipoTrabajoService
    {
        private readonly ProjectDbContext _projectDbContext;

        public TipoTrabajoService(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public bool Add(TipoTrabajo model)
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
                _projectDbContext.Entry(new TipoTrabajo { Id = id }).State = EntityState.Deleted;
                _projectDbContext.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public TipoTrabajo Get(int id)
        {
            var result = new TipoTrabajo();
            try
            {
                result = _projectDbContext.TipoTrabajo.Single(x => x.Id == id);
            }
            catch (Exception e)
            {
                
            }
            return result;
        }

        public IEnumerable<TipoTrabajo> GetAll()
        {
            var result = new List<TipoTrabajo>();
            try
            {
                result = _projectDbContext.TipoTrabajo.ToList();
            }catch(Exception e)
            {

            }
            return result;
        }

        public bool Update(TipoTrabajo model)
        {
            try
            {
                var originalModel = _projectDbContext.TipoTrabajo.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Nombre = model.Nombre;

                _projectDbContext.Update(originalModel);
                _projectDbContext.SaveChanges();

            } catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
