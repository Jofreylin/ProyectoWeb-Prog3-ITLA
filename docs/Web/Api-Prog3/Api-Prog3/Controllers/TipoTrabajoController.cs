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
    public class TipoTrabajoController : Controller
        {
            private readonly ITipoTrabajoService _ITipoTrabajoService;

            public TipoTrabajoController(ITipoTrabajoService TipoTrabajoService)
            {
                _ITipoTrabajoService = TipoTrabajoService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<TipoTrabajo>> GetAll()
            {
                return Ok(_ITipoTrabajoService.GetAll());
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                return Ok(_ITipoTrabajoService.Get(id));
            }

            [HttpPost]
            public IActionResult Add([FromBody] TipoTrabajo model)
            {
                return Ok(_ITipoTrabajoService.Add(model));
            }

            [HttpPut]
            public IActionResult Update([FromBody] TipoTrabajo model)
            {
                return Ok(_ITipoTrabajoService.Update(model));
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                return Ok(_ITipoTrabajoService.Delete(id));
            }
        }
    }
       

