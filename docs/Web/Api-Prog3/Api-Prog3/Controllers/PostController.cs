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
    public class PostController : Controller
        {
            private readonly IPostService _IPostService;

            public PostController(IPostService PostService)
            {
                _IPostService = PostService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<Post>> GetAll()
            {
                return Ok(_IPostService.GetAll());
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                return Ok(_IPostService.Get(id));
            }

            [HttpPost]
            public IActionResult Add([FromBody] Post model)
            {
                return Ok(_IPostService.Add(model));
            }

            [HttpPut]
            public IActionResult Update([FromBody] Post model)
            {
                return Ok(_IPostService.Update(model));
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                return Ok(_IPostService.Delete(id));
            }
        }
    
}
