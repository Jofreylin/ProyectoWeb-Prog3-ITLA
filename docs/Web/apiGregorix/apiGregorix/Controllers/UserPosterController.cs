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
    public class UserPosterController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserPosterController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPoster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPoster>>> GetUserPosters()
        {
            return await _context.UserPosters.ToListAsync();
        }

        // GET: api/UserPoster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPoster>> GetUserPoster(int id)
        {
            var userPoster = await _context.UserPosters.FindAsync(id);

            if (userPoster == null)
            {
                return NotFound();
            }

            return userPoster;
        }

        // PUT: api/UserPoster/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutUserPoster(UserPoster userPoster)
        {
            var original = await _context.UserPosters.FindAsync(userPoster.Id);

            if (original == null)
            {
                return NotFound();
            }

            original.NombreCalle = userPoster.NombreCalle;
            original.NombreCiudad = userPoster.NombreCiudad;
            original.NombreEmpresa = userPoster.NombreEmpresa;
            original.NombrePais = userPoster.NombrePais;
            original.Contra = userPoster.Contra;
            original.Email = userPoster.Email;

            _context.Entry(original).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPosterExists(userPoster.Id))
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

        // POST: api/UserPoster
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserPoster>> PostUserPoster(UserPoster userPoster)
        {
            _context.UserPosters.Add(userPoster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPoster", new { id = userPoster.Id }, userPoster);
        }

        // DELETE: api/UserPoster/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPoster>> DeleteUserPoster(int id)
        {
            var userPoster = await _context.UserPosters.FindAsync(id);
            if (userPoster == null)
            {
                return NotFound();
            }

            _context.UserPosters.Remove(userPoster);
            await _context.SaveChangesAsync();

            return userPoster;
        }

        private bool UserPosterExists(int id)
        {
            return _context.UserPosters.Any(e => e.Id == id);
        }
    }
}
