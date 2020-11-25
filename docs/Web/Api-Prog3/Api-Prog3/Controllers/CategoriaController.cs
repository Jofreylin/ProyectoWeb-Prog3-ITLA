using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;

namespace Api_Prog3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _ICategoriaService;
        private readonly ProjectDbContext _dbContext;

        public CategoriaController(ICategoriaService iCategoriaService, ProjectDbContext db)
        {
            _ICategoriaService = iCategoriaService;
            _dbContext = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAll()
        {
            return Ok(_ICategoriaService.GetAll());
        }

        [HttpGet("{id}")]
        public  IActionResult Get(int id)
        {
            return Ok(_ICategoriaService.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody]Categoria model)
        {
            try
            {
                _dbContext.Categoria.Add(model);
                _dbContext.SaveChanges();
            }catch(Exception e)
            {
                return StatusCode(500, e);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]Categoria model)
        {
            return Ok(_ICategoriaService.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_ICategoriaService.Delete(id));
        }
    }
}
