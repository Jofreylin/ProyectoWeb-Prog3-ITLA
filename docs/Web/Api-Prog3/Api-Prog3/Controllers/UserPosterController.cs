using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using Persistence;

namespace Api_Prog3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPosterController : Controller
    {
         private readonly IUserPosterService _IUserPosterService;
        private readonly ProjectDbContext _dbcontext;

        public UserPosterController(IUserPosterService UserPosterService, ProjectDbContext _db)
        {
            _IUserPosterService = UserPosterService;
            _dbcontext = _db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserPoster>> GetAll()
        {
            return Ok(_IUserPosterService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_IUserPosterService.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody]UserPoster model)
        {
            try
            {
                _dbcontext.UserPoster.Add(model);
                _dbcontext.SaveChanges();
            }catch(Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserPoster model)
        {
            return Ok(_IUserPosterService.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_IUserPosterService.Delete(id));
        }
    }
    }

