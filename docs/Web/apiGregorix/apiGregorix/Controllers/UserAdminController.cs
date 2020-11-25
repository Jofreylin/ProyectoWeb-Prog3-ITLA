using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiGregorix.Context;
using apiGregorix.Models;

namespace apiGregorix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAdminController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserAdminController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdmin>>> GetUserAdmins()
        {
            return await _context.UserAdmins.ToListAsync();
        }

        // GET: api/UserAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAdmin>> GetUserAdmin(int id)
        {
            var userAdmin = await _context.UserAdmins.FindAsync(id);

            if (userAdmin == null)
            {
                return NotFound();
            }

            return userAdmin;
        }

        // PUT: api/UserAdmin/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutUserAdmin(UserAdmin userAdmin)
        {
            var original = await _context.UserAdmins.FindAsync(userAdmin.Id);

            if (original == null)
            {
                return NotFound();
            }

            original.Nombre = userAdmin.Nombre;
            original.Usuario = userAdmin.Usuario;
            original.Contraseña = userAdmin.Contraseña;
            original.Email = userAdmin.Email;

            _context.Entry(original).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAdminExists(userAdmin.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserAdmin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserAdmin>> PostUserAdmin(UserAdmin userAdmin)
        {
            _context.UserAdmins.Add(userAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAdmin", new { id = userAdmin.Id }, userAdmin);
        }

        // DELETE: api/UserAdmin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAdmin>> DeleteUserAdmin(int id)
        {
            var userAdmin = await _context.UserAdmins.FindAsync(id);
            if (userAdmin == null)
            {
                return NotFound();
            }

            _context.UserAdmins.Remove(userAdmin);
            await _context.SaveChangesAsync();

            return userAdmin;
        }

        private bool UserAdminExists(int id)
        {
            return _context.UserAdmins.Any(e => e.Id == id);
        }
    }
}
