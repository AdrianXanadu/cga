using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CGA_Server.Models;

namespace CGA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly CGAContext _context;

        public ScoresController()
        {
            _context = Context._CGAContext;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScore()
        {
          if (_context.Score == null)
          {
              return NotFound();
          }

            return await _context.Score.Include(s => s.PidNavigation).ToListAsync();
        }

        // GET: api/Scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
          if (_context.Score == null)
          {
              return NotFound();
          }
            var score = await _context.Score.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }


        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore([FromBody] Score score)
        {
            var pl = _context.Player.FirstOrDefault(p => p.Id == score.PidNavigation.Id);
            score.PidNavigation = pl;

            if (_context.Score == null)
          {
              return Problem("Entity set 'CGAContext.Score'  is null.");
          }
            _context.Score.Add(score);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScore", new { id = score.Sid }, score);
        }

        // DELETE: api/Scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            if (_context.Score == null)
            {
                return NotFound();
            }
            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            _context.Score.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("max")]
        public int GetHighestScoreId()
        {
            return _context.Score.Max(s => s.Sid);
        }


        private bool ScoreExists(int id)
        {
            return (_context.Score?.Any(e => e.Sid == id)).GetValueOrDefault();
        }
    }
}
