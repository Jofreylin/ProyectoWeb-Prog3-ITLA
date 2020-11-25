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
    public class TipoTrabajoController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TipoTrabajoController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoTrabajo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTrabajo>>> GetTipoTrabajos()
        {
            return await _context.TipoTrabajos.ToListAsync();
        }

        // GET: api/TipoTrabajo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTrabajo>> GetTipoTrabajo(int id)
        {
            var tipoTrabajo = await _context.TipoTrabajos.FindAsync(id);

            if (tipoTrabajo == null)
            {
                return NotFound();
            }

            return tipoTrabajo;
        }

        // PUT: api/TipoTrabajo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutTipoTrabajo( TipoTrabajo tipoTrabajo)
        {
            var original = await _context.TipoTrabajos.FindAsync(tipoTrabajo.Id);

            if(original == null)
            {
                return NotFound();
            }

            original.Nombre = tipoTrabajo.Nombre;

            _context.Entry(original).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTrabajoExists(tipoTrabajo.Id))
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

        // POST: api/TipoTrabajo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TipoTrabajo>> PostTipoTrabajo(TipoTrabajo tipoTrabajo)
        {
            _context.TipoTrabajos.Add(tipoTrabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoTrabajo", new { id = tipoTrabajo.Id }, tipoTrabajo);
        }

        // DELETE: api/TipoTrabajo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoTrabajo>> DeleteTipoTrabajo(int id)
        {
            var tipoTrabajo = await _context.TipoTrabajos.FindAsync(id);
            if (tipoTrabajo == null)
            {
                return NotFound();
            }

            _context.TipoTrabajos.Remove(tipoTrabajo);
            await _context.SaveChangesAsync();

            return tipoTrabajo;
        }

        private bool TipoTrabajoExists(int id)
        {
            return _context.TipoTrabajos.Any(e => e.Id == id);
        }
    }
}
