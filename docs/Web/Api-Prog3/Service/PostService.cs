using System;
using System.Collections.Generic;
using System.Text;
using Persistence;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Service
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Post Get(int id);
        bool Add(Post model);
        bool Update(Post model);
        bool Delete(int id);
    }
    public class PostService : IPostService
    {
        private readonly ProjectDbContext _projectDbContext;

        public PostService(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public bool Add(Post model)
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
                _projectDbContext.Entry(new Post { Id = id }).State = EntityState.Deleted;
                _projectDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public Post Get(int id)
        {
            var result = new Post();

            try
            {
                result = _projectDbContext.Post.Single(x => x.Id == id);
            }
            catch (Exception e)
            {

            }

            return result;
        }

        public IEnumerable<Post> GetAll()
        {
            var result = new List<Post>();

            try
            {
                result = _projectDbContext.Post.ToList();
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public bool Update(Post model)
        {
            try
            {
                var originalModel = _projectDbContext.Post.Single(x => x.Id == model.Id);

                originalModel.Id = model.Id;
                originalModel.Logo = model.Logo;
                originalModel.NombreCalle = model.NombreCalle;
                originalModel.NombreCategoria = model.NombreCategoria;
                originalModel.NombreCiudad = model.NombreCiudad;
                originalModel.NombrePais = model.NombrePais;
                originalModel.NombrePosicion = model.NombrePosicion;
                originalModel.NombreTipoTrabajo = model.NombreTipoTrabajo;
                originalModel.Poster = model.Poster;
                originalModel.Descripcion = model.Descripcion;
                originalModel.DireccionUrl = model.DireccionUrl;

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
