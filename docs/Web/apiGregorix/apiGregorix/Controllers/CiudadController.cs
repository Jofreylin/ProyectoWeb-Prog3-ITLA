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
    public class CiudadController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CiudadController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Ciudad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciudad>>> GetCiudads()
        {
            return await _context.Ciudads.ToListAsync();
        }

        // GET: api/Ciudad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ciudad>> GetCiudad(int id)
        {
            var ciudad = await _context.Ciudads.FindAsync(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            return ciudad;
        }

        // PUT: api/Ciudad/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutCiudad(Ciudad ciudad)
        {
            var original = await _context.Ciudads.FindAsync(ciudad.Id);

            if (original == null)
            {
                return NotFound();
            }

            original.Nombre = ciudad.Nombre;
            original.NombrePais = ciudad.NombrePais;

            _context.Entry(original).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadExists(ciudad.Id))
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

        // POST: api/Ciudad
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ciudad>> PostCiudad(Ciudad ciudad)
        {
            _context.Ciudads.Add(ciudad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiudad", new { id = ciudad.Id }, ciudad);
        }

        // DELETE: api/Ciudad/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ciudad>> DeleteCiudad(int id)
        {
            var ciudad = await _context.Ciudads.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _context.Ciudads.Remove(ciudad);
            await _context.SaveChangesAsync();

            return ciudad;
        }

        private bool CiudadExists(int id)
        {
            return _context.Ciudads.Any(e => e.Id == id);
        }
    }
}
