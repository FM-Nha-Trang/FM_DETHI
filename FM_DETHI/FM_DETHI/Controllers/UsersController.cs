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

namespace FM_DETHI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }
        
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            if(!UsernameExists(user.Username))
            {
                MD5 md5Hash = MD5.Create();
                Users user_new = new Users();

                user_new.Username = user.Username;
                user_new.Pass = GetMd5Hash(md5Hash, user.Pass);
                user_new.First_name = user.First_name.ToString();
                user_new.Last_name = user.Last_name.ToString();
                user_new.Age = user.Age;
                user_new.Gender = user.Gender.ToString();

                _context.Users.Add(user_new);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetUser", new { id = user_new.Id }, user_new);
                Response.StatusCode = 201;
                return Content("{\"Status\":\"201\",\"Message\":\"Tạo tài khoản thành công\", \"ID\" : \"" + user_new.Id+"\"}");
            }
            Response.StatusCode = 500;
            return Content("{\"Status\":\"500\",\"Message\":\"Tên đăng nhập đã tồn tại!\"}");
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        // GET: api/Users/Login
        [Route("Login")]
        [HttpGet]
        public ActionResult<Users> Login(Users user)
        {
            //Check null
            if(user.Username=="")
            {
                Response.StatusCode = 500;
                return Content("{\"StatusCode\":\"500\",\"Message\":\"Tên đăng nhập không được để trống!\"}");
            }
            if (user.Pass == "")
            {
                Response.StatusCode = 500;
                return Content("{\"StatusCode\":\"500\",\"Message\":\"Mật khẩu không được để trống!\"}");
            }

            //check username exists
            //if true, verify pass, else return message error
            if (UsernameExists(user.Username))
            {
                MD5 md5Hash = MD5.Create();
                if (VerifyPassword(md5Hash,user.Pass))
                {
                    Response.StatusCode = 200;
                    var user_login = _context.Users
                                            .Where(s => s.Username == user.Username)
                                            .ToList();
                    return Content("{\"StatusCode\":\"200\",\"Message\":\"Đăng nhập thành công!\",\"Data\":"+ JsonConvert.SerializeObject(user_login) + "}");
                } else
                {
                    Response.StatusCode = 500;
                    return Content("{"StatusCode\":\"500\",\"Message\":\"Mật khẩu không chính xác!\"}");
                }
               
            }
            Response.StatusCode = 200;
            return Content("{\"StatusCode\":\"500\",\"Message\":\"Tên tài khoản không chính xác!\"}");

        }

        private bool VerifyPassword(MD5 md5Hash, string pw)
        {
            // Hash the input.
            string hash_now = GetMd5Hash(md5Hash, pw);
            return CheckPassExists(hash_now);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }

        private bool CheckPassExists(string pw_hash)
        {
            return _context.Users.Any(e => e.Pass == pw_hash);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
