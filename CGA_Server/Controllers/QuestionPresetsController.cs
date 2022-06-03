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
    public class QuestionPresetsController : ControllerBase
    {
        private readonly CGAContext _context;

        public QuestionPresetsController()
        {
            _context = new CGAContext();
        }

        // GET: api/QuestionPresets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionPreset>>> GetQuestionPreset()
        {
          if (_context.QuestionPreset == null)
          {
              return NotFound();
          }
            return await _context.QuestionPreset.ToListAsync();
        }

        // GET: api/QuestionPresets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionPreset>> GetQuestionPreset(int id)
        {
          if (_context.QuestionPreset == null)
          {
              return NotFound();
          }
            var questionPreset = await _context.QuestionPreset.FindAsync(id);

            if (questionPreset == null)
            {
                return NotFound();
            }

            return questionPreset;
        }

        // PUT: api/QuestionPresets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionPreset(int id, [FromBody] QuestionPreset questionPreset)
        {
            if (id != questionPreset.Qid)
            {
                return BadRequest();
            }

            _context.Entry(questionPreset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionPresetExists(id))
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

        // POST: api/QuestionPresets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionPreset>> PostQuestionPreset([FromBody] QuestionPreset questionPreset)
        {
          if (_context.QuestionPreset == null)
          {
              return Problem("Entity set 'CGAContext.QuestionPreset'  is null.");
          }
            _context.QuestionPreset.Add(questionPreset);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionPreset", new { id = questionPreset.Qid }, questionPreset);
        }

        // DELETE: api/QuestionPresets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionPreset(int id)
        {
            if (_context.QuestionPreset == null)
            {
                return NotFound();
            }
            var questionPreset = await _context.QuestionPreset.FindAsync(id);
            if (questionPreset == null)
            {
                return NotFound();
            }

            _context.QuestionPreset.Remove(questionPreset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionPresetExists(int id)
        {
            return (_context.QuestionPreset?.Any(e => e.Qid == id)).GetValueOrDefault();
        }
    }
}
