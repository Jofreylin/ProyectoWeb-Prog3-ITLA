using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Model;

namespace Api_Prog3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAdminController : Controller
    {
        private readonly IUserAdminService _userAdminService;

        public UserAdminController(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserAdmin>> GetAll()
        {
            return Ok(_userAdminService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userAdminService.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody]UserAdmin model)
        {
            return Ok(_userAdminService.Add(model));
        }

        [HttpPut]
        public IActionResult Update([FromBody]UserAdmin model)
        {
            return Ok(_userAdminService.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_userAdminService.Delete(id));
        }
    }
}