using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Api_Prog3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadController : Controller
    {
        private readonly ICiudadService _ICiudadService;

        public CiudadController(ICiudadService ciudadService)
        {
            _ICiudadService = ciudadService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ciudad>> GetAll()
        {
            return Ok(_ICiudadService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_ICiudadService.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody]Ciudad model)
        {
            return Ok(_ICiudadService.Add(model));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Ciudad model)
        {
            return Ok(_ICiudadService.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_ICiudadService.Delete(id));
        }
    }
}