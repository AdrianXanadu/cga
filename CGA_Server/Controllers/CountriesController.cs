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
    public class CountriesController : ControllerBase
    {
        private readonly CGAContext _context;

        public CountriesController()
        {
            _context = Context._CGAContext;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
          if (_context.Country == null)
          {
              return NotFound();
          }
            return await _context.Country.Include(c => c.Iid).Include(c => c.Location).ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
          if (_context.Country == null)
          {
              return NotFound();
          }
            var country = await _context.Country.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry([FromBody] Country country)
        {
          if (_context.Country == null)
          {
              return Problem("Entity set 'CGAContext.Country'  is null.");
          }
            _context.Country.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Cid }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_context.Country == null)
            {
                return NotFound();
            }
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("max")]
        public int GetHighestCountryId()
        {
            return _context.Country.Max(x => x.Cid);
        }

        private bool CountryExists(int id)
        {
            return (_context.Country?.Any(e => e.Cid == id)).GetValueOrDefault();
        }
    }
}
