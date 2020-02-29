using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwittAPI.Models;

namespace TwittAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly TwittContext _context;

        public ReactionsController(TwittContext context)
        {
            _context = context;
        }

        // GET: api/Reactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reaction>>> GetReaction()
        {
            return await _context.Reaction.ToListAsync();
        }

        // GET: api/Reactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reaction>> GetReaction(int id)
        {
            var reaction = await _context.Reaction.FindAsync(id);

            if (reaction == null)
            {
                return NotFound();
            }

            return reaction;
        }

        // PUT: api/Reactions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReaction(int id, Reaction reaction)
        {
            if (id != reaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(reaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactionExists(id))
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

        // POST: api/Reactions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Reaction>> PostReaction(Reaction reaction)
        {
            _context.Reaction.Add(reaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReaction", new { id = reaction.Id }, reaction);
        }

        // DELETE: api/Reactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reaction>> DeleteReaction(int id)
        {
            var reaction = await _context.Reaction.FindAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }

            _context.Reaction.Remove(reaction);
            await _context.SaveChangesAsync();

            return reaction;
        }

        private bool ReactionExists(int id)
        {
            return _context.Reaction.Any(e => e.Id == id);
        }
    }
}
