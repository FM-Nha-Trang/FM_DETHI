using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DETHI.Models;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using FM_DETHI.Models;
using System.Globalization;

namespace FM_DETHI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class History_AnswerController : ControllerBase
    {
        private readonly UserContext _context;

        public History_AnswerController(UserContext context)
        {
            _context = context;
        }

        // GET: api/History_Answer
        [Route("List")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History_Answer>>> GetHistory_Answers()
        {
            return await _context.History_Answer.ToListAsync();
        }

        // GET: api/History_Answer
        [Route("List/Test/{id}")]
        [HttpGet("{id}")]
        public ActionResult<History_Answer> GetHistory_AnswersByTestCode(string id)
        {
            if (!_context.History_Answer.Any(s => s.test_code == id))
            {
                return Ok("{\"StatusCode\" : \"200\", \"Message\" : \"Mã đề này chưa có ai đáp đề!\"}");
            }
            var list = _context.History_Answer
                                .Where(s => s.test_code == id)
                                .ToList();
            return Ok("{\"StatusCode\" : \"200\", \"Message\" : \"Thành công!\", \"Result\" : " + JsonConvert.SerializeObject(list) + "}");
        }

        // GET: api/History_Answer
        [Route("List/User/{id}")]
        [HttpGet("{id}")]
        public ActionResult<History_Answer> GetHistory_AnswersByUserAnswer(int id)
        {
            if (!_context.History_Answer.Any(s => s.userid == id))
            {
                return Ok("{\"StatusCode\" : \"200\", \"Message\" : \"Bạn chưa đáp đề nào!\"}");
            }
            var list = _context.History_Answer
                                .Join(_context.Tests, h => h.test_code, t => t.test_code, (h, t) => new { h.id, h.test_code, h.userid, h.date_answer, h.point, t.title })
                                .Where(s => s.userid == id)
                                .ToList();
            return Ok("{\"StatusCode\" : \"200\", \"Message\" : \"Thành công!\", \"Result\" : " + JsonConvert.SerializeObject(list) + "}");
        }

        // GET: api/History_Answer/5
        [Route("Get/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<History_Answer>> GetHistory_Answer(int id)
        {
            var history_Answer = await _context.History_Answer.FindAsync(id);

            if (history_Answer == null)
            {
                return NotFound();
            }

            return history_Answer;
        }

        // PUT: api/History_Answer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Update/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory_Answer(int id, History_Answer history_Answer)
        {
            if (id != history_Answer.id)
            {
                return BadRequest();
            }

            _context.Entry(history_Answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!History_AnswerExists(id))
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

        // POST: api/History_Answer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<History_Answer>> PostHistory_Answer(History_Answer history_Answer)
        {
            bool user_id_correct = _context.Users.Any(e => e.Id == history_Answer.userid);
            bool test_code_correct = _context.Tests.Any(e => e.test_code == history_Answer.test_code);
            bool isAnswer = _context.History_Answer.Any(e=> e.userid == history_Answer.userid 
                                                            && e.test_code == history_Answer.test_code);
            if (!user_id_correct)
            {
                Response.StatusCode = 500;
                return Content("{\"StatusCode\" : 500 , \"Message\" : \"Id người trả lời (userid) không khả dụng!\"}");
            }
            if(!test_code_correct)
            {
                Response.StatusCode = 500;
                return Content("{\"StatusCode\" : 500 , \"Message\" : \"Mã đề (Test_code) không khả dụng!\"}");
            }

            if(isAnswer)
            {
                var temp = _context.History_Answer
                                            .Where(s => s.test_code == history_Answer.test_code)
                                            .Where(s => s.userid == history_Answer.userid)
                                            .Select(s => s.id)
                                            .First();
                history_Answer.id = (int)temp;
                _context.Entry(history_Answer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                Response.StatusCode = 200;
                return Content("{\"StatusCode\" : 200 , \"Message\" : \"Cập nhập lần đáp đề thành công!\" , \"id\" :" + temp+"}");
            }

            _context.History_Answer.Add(history_Answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistory_Answer", new { id = history_Answer.id });
        }

        // DELETE: api/History_Answer/5
        [Route("Delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<History_Answer>> DeleteHistory_Answer(int id)
        {
            var history_Answer = await _context.History_Answer.FindAsync(id);
            if (history_Answer == null)
            {
                return NotFound();
            }

            _context.History_Answer.Remove(history_Answer);
            await _context.SaveChangesAsync();

            return history_Answer;
        }

        private bool History_AnswerExists(int id)
        {
            return _context.History_Answer.Any(e => e.id == id);
        }
    }
}
