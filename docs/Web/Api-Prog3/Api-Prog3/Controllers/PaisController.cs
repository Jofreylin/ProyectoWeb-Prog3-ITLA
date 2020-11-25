using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api_Prog3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisController : Controller
    {
        private readonly IPaisService _IPaisService;
        private readonly ProjectDbContext _context;

        public PaisController(IPaisService paisService, ProjectDbContext _c)
        {
            _IPaisService = paisService;
            _context = _c;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetAll()
        {
            return await _context.Pais.ToListAsync();

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_IPaisService.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody]Pais model)
        {
            return Ok(_IPaisService.Add(model));
        }

        [HttpPut]
        public IActionResult Update([FromBody]Pais model)
        {
            return Ok(_IPaisService.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_IPaisService.Delete(id));
        }
    }
}