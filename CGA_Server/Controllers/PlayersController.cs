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
    public class PlayersController : ControllerBase
    {
        private readonly CGAContext _context;

        public PlayersController()
        {
            _context = new CGAContext();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchPlayer([FromBody] Player player, int id)
        {
            if (_context.Player == null)
            {
                return NotFound();
            }

            if (PlayerExists(id))
            {
                var newPlayer = await _context.Player.FindAsync(id);
                
                newPlayer.Name = player.Name;
                newPlayer.Password = player.Password;

                await _context.SaveChangesAsync();

                return Ok();
            } else
            {
                return NotFound();
            }
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
        {
          if (_context.Player == null)
          {
              return NotFound();
          }
            return await _context.Player.Include(p => p.Score).ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayerById(int id)
        {
          if (_context.Player == null)
          {
              return NotFound();
          }
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Player>> GetPlayerByName(string name)
        {
            if (_context.Player == null)
            {
                return NotFound();
            }
            var player = _context.Player.Where(p => p.Name == name).SingleOrDefault();

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, [FromBody] Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromBody] Player player)
        {
          if (_context.Player == null)
          {
              return Problem("Entity set 'CGAContext.Player'  is null.");
          }
            _context.Player.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (_context.Player == null)
            {
                return NotFound();
            }
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("max")]
        public int GetHighestPlayerId()
        {
            return _context.Player.Max(x => x.Id);
        }

        private bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
