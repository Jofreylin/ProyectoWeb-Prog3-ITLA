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
    public class PaisController : Controller
    {
        private readonly IPaisService _IPaisService;

        public PaisController(IPaisService paisService)
        {
            _IPaisService = paisService;
        } 

        [HttpGet]
        public ActionResult<IEnumerable<Pais>> GetAll()
        {
            return Ok(_IPaisService.GetAll());
        }

        [HttpGet("id")]
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

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            return Ok(_IPaisService.Delete(id));
        }
    }
}