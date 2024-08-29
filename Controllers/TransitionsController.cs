using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusFlowAPI.Models;

namespace StatusFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Transitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transition>>> GetTransitions()
        {
            return await _context.Transitions.ToListAsync();
        }

        // GET: api/Transitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transition>> GetTransition(int id)
        {
            var transition = await _context.Transitions.FindAsync(id);

            if (transition == null)
            {
                return NotFound();
            }

            return transition;
        }

        // PUT: api/Transitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransition(int id, Transition transition)
        {
            if (id != transition.TransitionId)
            {
                return BadRequest();
            }

            _context.Entry(transition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransitionExists(id))
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

        // POST: api/Transitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transition>> PostTransition(Transition transition)
        {
            _context.Transitions.Add(transition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransition", new { id = transition.TransitionId }, transition);
        }

        // DELETE: api/Transitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransition(int id)
        {
            var transition = await _context.Transitions.FindAsync(id);
            if (transition == null)
            {
                return NotFound();
            }

            _context.Transitions.Remove(transition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransitionExists(int id)
        {
            return _context.Transitions.Any(e => e.TransitionId == id);
        }
    }
}
