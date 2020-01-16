using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DETHI.Models;
using FM_DETHI.Models;

namespace FM_DETHI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly UserContext _context;

        public QuestionsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Questions/List
        [Route("List")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        // GET: api/Questions/5
        [Route("Get/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // GET: api/Questions/5
        [Route("List/{id}")]
        [HttpGet("{id}")]
        public ActionResult<Questions> GetQuestionsByTestCode(string id)
        {
            var questions = _context.Questions
                                            .Where(s => s.Test_code == id)
                                            .ToList(); ;

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Update/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.id)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
            _context.Questions.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestions", new { id = questions.id }, questions);
        }

        // DELETE: api/Questions/5
        [Route("Delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questions>> DeleteQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return questions;
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.id == id);
        }
    }
}
