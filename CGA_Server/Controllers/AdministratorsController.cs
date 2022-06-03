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
    public class AdministratorsController : ControllerBase
    {
        private readonly CGAContext _context;

        public AdministratorsController()
        {
            _context = new CGAContext();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchAdmin([FromBody] Administrator administrator, int id)
        {
            if (_context.Player == null)
            {
                return NotFound();
            }

            if (AdministratorExists(id))
            {
                var newAdministrator = await _context.Administrator.FindAsync(id);

                newAdministrator.Name = administrator.Name;
                newAdministrator.Password = administrator.Password;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Administrators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAdministrator()
        {
          if (_context.Administrator == null)
          {
              return NotFound();
          }
            return await _context.Administrator.ToListAsync();
        }

        // GET: api/Administrators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministrator(int id)
        {
          if (_context.Administrator == null)
          {
              return NotFound();
          }
            var administrator = await _context.Administrator.FindAsync(id);

            if (administrator == null)
            {
                return NotFound();
            }

            return administrator;
        }

        [HttpGet("name/{name}")]
        public ActionResult<Administrator> GetAdministratorByName(string name)
        {
            if (_context.Administrator == null)
            {
                return NotFound();
            }
            var admin = _context.Administrator.Where(a => a.Name == name).SingleOrDefault();

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Administrators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(int id, [FromBody] Administrator administrator)
        {
            if (id != administrator.Id)
            {
                return BadRequest();
            }

            _context.Entry(administrator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administrators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Administrator>> PostAdministrator([FromBody] Administrator administrator)
        {
          if (_context.Administrator == null)
          {
              return Problem("Entity set 'CGAContext.Administrator'  is null.");
          }
            _context.Administrator.Add(administrator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrator", new { id = administrator.Id }, administrator);
        }

        // DELETE: api/Administrators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(int id)
        {
            if (_context.Administrator == null)
            {
                return NotFound();
            }
            var administrator = await _context.Administrator.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            _context.Administrator.Remove(administrator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministratorExists(int id)
        {
            return (_context.Administrator?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
