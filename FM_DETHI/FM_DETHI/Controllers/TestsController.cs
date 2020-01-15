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
    public class TestsController : ControllerBase
    {
        private readonly UserContext _context;

        public TestsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tests>>> GetTests()
        {
            return await _context.Tests.ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tests>> GetTests(string id)
        {
            var tests = await _context.Tests.FindAsync(id);

            if (tests == null)
            {
                return NotFound();
            }

            return tests;
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTests(string id, Tests tests)
        {
            if (id != tests.test_code)
            {
                return BadRequest();
            }

            _context.Entry(tests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestsExists(id))
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

        // POST: api/Tests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tests>> PostTests(Tests tests)
        {
            _context.Tests.Add(tests);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TestsExists(tests.test_code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTests", new { id = tests.test_code }, tests);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tests>> DeleteTests(string id)
        {
            var tests = await _context.Tests.FindAsync(id);
            if (tests == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(tests);
            await _context.SaveChangesAsync();

            return tests;
        }

        private bool TestsExists(string id)
        {
            return _context.Tests.Any(e => e.test_code == id);
        }
    }
}
