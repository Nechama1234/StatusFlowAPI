using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using NuGet.Versioning;
using StatusFlowAPI.Models;

namespace StatusFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.StatusId == id);
        }

        private HashSet<string> FindOrphanStatuses(string startStatus)
        {
            HashSet<string> reachableStates = new HashSet<string>();
            GetReachableStatuses(startStatus,ref reachableStates);

            HashSet<string> orphanStatuses = _context.Statuses
               .Select(status => status.StatusId.ToString()).ToHashSet();
            orphanStatuses.ExceptWith(reachableStates);
            return orphanStatuses;
        }

  

        //find by dfs search
        private void GetReachableStatuses(string initialStatus,ref HashSet<string> reachableStatuses)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(initialStatus);
            reachableStatuses.Add(initialStatus);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                List<Transition> transitions = _context.Transitions.ToList();
                foreach (var transition in transitions.Where(t => t.FromStatusId.ToString() == current))
                {
                    if (!reachableStatuses.Contains(transition.ToStatusId.ToString()))
                    {
                        reachableStatuses.Add(transition.ToStatusId.ToString());
                        queue.Enqueue(transition.ToStatusId.ToString());
                    }
                }
            }
        }


        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {

            return await _context.Statuses.ToListAsync();
        }

        // GET: api/Status
        [HttpGet("get-orphan-statuses")]
        public async Task<ActionResult<IEnumerable<Status>>> GetOrphanStatuses()
        {
            HashSet<string> OrphanStatuses = new HashSet<string>();
            OrphanStatuses = FindOrphanStatuses("34");
            var matchingStatuses = _context.Statuses
           .Where(status => OrphanStatuses.Contains(status.StatusId.ToString()));
           return await matchingStatuses.ToListAsync();
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }



        // PUT: api/Status/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.StatusId)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Status
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.StatusId }, status);
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try
            {

           
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            var transitionsFrom = _context.Transitions
                .Where(t => t.FromStatusId == id);

            _context.Transitions.RemoveRange(transitionsFrom);

            var transitionsTo = _context.Transitions
                .Where(t => t.ToStatusId == id);

            _context.Transitions.RemoveRange(transitionsTo);

           
            await _context.SaveChangesAsync();
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                var exx = ex.Message;
                throw;
            }
        }

      

    }
}
