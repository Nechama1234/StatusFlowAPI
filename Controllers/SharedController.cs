using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusFlowAPI.Models;

namespace StatusFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SharedController(ApplicationDbContext context)
        {
            _context = context;
        }

          [HttpDelete("clear-tables")]
        public async Task<IActionResult> ClearTables()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
              
               _context.Transitions.RemoveRange(_context.Transitions);
                await _context.SaveChangesAsync();

                _context.Statuses.RemoveRange(_context.Statuses);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new { message = "Tables cleared successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = "Error clearing tables", error = ex.Message });
            }
        }
    }
}
