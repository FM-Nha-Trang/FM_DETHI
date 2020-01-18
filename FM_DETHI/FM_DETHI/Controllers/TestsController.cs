using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DETHI.Models;
using FM_DETHI.Models;
using System.Globalization;

namespace FM_DETHI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly UserContext _context;

        public TestsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Tests/List
        [Route("List")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tests>>> GetTests()
        {
            List<Tests> list =  await _context.Tests.ToListAsync();
            return list;
        }

        // GET: api/Tests/List
        [Route("List/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tests>>> GetTestList(int id)
        {
            var list = await _context.Tests.ToListAsync();
            var h_list =  _context.History_Answer
                                            .Where(s => s.userid == id)
                                            .ToArray();
            var result = new List<ResultTest>();

            if (h_list.Count() == 0)
            {
                foreach (var lst in list)
                {
                    ResultTest content_add = new ResultTest(
                        lst.test_code,
                        lst.user_create,
                        lst.date_create.ToString(),
                        lst.title,
                        false,
                        null
                    );
                    result.Add(content_add);
                    content_add = null;
                }
                
            } else
            {
                foreach (var lst in list)
                {
                    History_Answer dataAnswer = new History_Answer();
                    ResultTest content_add;
                    bool check = false;
                    foreach (var h_lst in h_list)
                    {
                        if (h_lst.test_code == lst.test_code)
                        {
                            check = true;
                            dataAnswer = h_lst;
                            break;
                        }
                    }
                    if(check)
                    {
                        content_add = new ResultTest(
                                lst.test_code,
                                lst.user_create,
                                lst.date_create.ToString(),
                                lst.title,
                                true,
                                dataAnswer
                            );
                        result.Add(content_add);
                        content_add = null;
                    } else
                    {
                        content_add = new ResultTest(
                                lst.test_code,
                                lst.user_create,
                                lst.date_create.ToString(),
                                lst.title,
                                false,
                                null
                            );
                        result.Add(content_add);
                        content_add = null;
                    }    
                }
            }            
            return Ok(result);
        }

        // GET: api/Tests/get/5
        [Route("Get/{id}")]
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

        // PUT: api/Tests/update/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Update/{id}")]
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

        // POST: api/Tests/add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<Tests>> PostTests(Tests tests)
        {
            do
            {
                tests.test_code = this.CreateTestCode((int)tests.user_create);
            } while (TestsExists(tests.test_code));

            DateTime dt =  DateTime.Now;
            DateTime datenow = DateTime.Parse(dt.ToString(), CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None);
            tests.date_create = datenow;

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

        // DELETE: api/Tests/delete/5
        [Route("Delete/{id}")]
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

        // DELETE: api/Tests/delete/5
        [Route("Check/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tests>> CheckTestCode(string id)
        {
            var tests = await _context.Tests.FindAsync(id);
            if (tests == null)
            {
                return Ok("{\"Result\": \"False\"}");
            }

            return Ok("{\"Result\" : \"True\"}");
        }


        public bool TestsExists(string id)
        {
            return _context.Tests.Any(e => e.test_code == id);
        }

        public string CreateTestCode(int id)
        {
            Random rand = new Random();
            return "TU" + id + "" + rand.Next(999999);
        }
    }
}
