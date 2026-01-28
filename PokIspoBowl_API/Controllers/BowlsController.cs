using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokIspoBowl_API.Data;
using PokIspoBowl_API.Model;

namespace PokIspoBowl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlsController : ControllerBase
    {
        private readonly PokIspoBowlContext _context;

        public BowlsController(PokIspoBowlContext context)
        {
            _context = context;
        }

        // GET: api/Bowls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bowl>>> GetBowls()
        {
            return await _context.Bowls.ToListAsync();
        }

        // GET: api/Bowls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bowl>> GetBowl(int id)
        {
            var bowl = await _context.Bowls.FindAsync(id);

            if (bowl == null)
            {
                return NotFound();
            }

            return bowl;
        }

        // PUT: api/Bowls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBowl(int id, Bowl bowl)
        {
            if (id != bowl.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(bowl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BowlExists(id))
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

        // POST: api/Bowls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bowl>> PostBowl(Bowl bowl)
        {
            _context.Bowls.Add(bowl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBowl", new { id = bowl.ProductId }, bowl);
        }

        // DELETE: api/Bowls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBowl(int id)
        {
            var bowl = await _context.Bowls.FindAsync(id);
            if (bowl == null)
            {
                return NotFound();
            }

            _context.Bowls.Remove(bowl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BowlExists(int id)
        {
            return _context.Bowls.Any(e => e.ProductId == id);
        }
    }
}
