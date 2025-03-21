using Dog.Models;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Dog;

namespace Dog.Controllers
{
    public class AccountController : ApiController
    {
        Models.Model1 db = new Models.Model1();

        //【方法一】產生隨機鹽值的方法
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
        // 【方法二】使用 Argon2id 處理密碼雜湊的方法
        public static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = salt; // 設定鹽值
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8提高效率
            argon2.Iterations = 4;//雜湊重複執行幾次
            argon2.MemorySize = 512 * 1024;
            return argon2.GetBytes(16);
        }
        // Hash 處理加鹽的密碼功能 (這裡使用 HMACSHA256，您也可以使用 Argon2id)
        //private byte[] HashPassword(string password, byte[] salt)
        //{
        //    using (var hmac = new HMACSHA256(salt))
        //    {
        //        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    }
        //}

        // 【方法三】驗證密碼是否正確的方法
        public static bool VerifyPassword(string inputPassword, byte[] storedHash, byte[] storedSalt)
        {
            var newHash = HashPassword(inputPassword, storedSalt);
            if (storedHash.Length != newHash.Length)
                return false; // 長度不同，直接回傳 false
            for (int i = 0; i < storedHash.Length; i++)
            {
                if (storedHash[i] != newHash[i])
                    return false; // 只要有一個位元組不同，就回傳 false
            }
            return true;
        }
        //private bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
        //{
        //    var computedHash = HashPassword(password, salt);
        //    return computedHash.SequenceEqual(storedHash); // 使用 LINQ 的 SequenceEqual
        //}
        // 【方法四】驗證密碼的另一個版本，接受 Base64 字串格式的參數
        public static bool VerifyPassword(string inputPassword, string storedHashBase64, string storedSaltBase64)
        {
            byte[] storedHash = Convert.FromBase64String(storedHashBase64);
            byte[] storedSalt = Convert.FromBase64String(storedSaltBase64);
            return VerifyPassword(inputPassword, storedHash, storedSalt);
        }
        [HttpPost]
        [Route("api/employee/login")]//員工登入
        public IHttpActionResult EmployeeLogin([FromBody] LoginDto loginDto)
        {
            try
            {
                // 查詢使用者是否存在
                var user = db.Employee?.FirstOrDefault(m => m.Account == loginDto.Account);
                if (user == null)
                {
                    return Content(HttpStatusCode.BadRequest, new { success = false, message = "帳號不存在" });
                }
                // 轉換 Base64 密碼與鹽值
                byte[] storedHash = Convert.FromBase64String(user.Password);
                byte[] storedSalt = Convert.FromBase64String(user.Salt);

                // 驗證密碼
                bool isValid = VerifyPassword(loginDto.Password, storedHash, storedSalt);
                if (!isValid)
                {
                    return Content(HttpStatusCode.BadRequest, new { success = false, message = "密碼錯誤" });
                }
                // ✅ 產生 JWT Token
                //JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
                //string jwtToken = jwtAuthUtil.GenerateToken_Employee(user.Name, user.Identity);


                return Ok(new
                {
                    success = true,
                    message = "登入成功",
                    //token = jwtToken
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new
                {
                    statusCode = 400,
                    code = -1,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
            [Route("api/register")]//新增使用者資料
            public IHttpActionResult ApiRegister([FromBody] RegisterDto registerDto)
            {
                // 1. 確保帳號不為空
                if (string.IsNullOrEmpty(registerDto.Account) || string.IsNullOrEmpty(registerDto.Password))
                {
                    return Content(HttpStatusCode.BadRequest, new { success = false, message = "帳號和密碼不能為空" });
                }

                // 2. 檢查帳號是否已存在
                var existingUser = db.Employee.FirstOrDefault(m => m.Account == registerDto.Account);
                if (existingUser != null)
                {
                    return Content(HttpStatusCode.BadRequest, new { success = false, message = "此帳號已被使用" });
                }

                // 3. 生成密碼雜湊與鹽值
                byte[] salt = CreateSalt();
                byte[] hashedPassword = HashPassword(registerDto.Password, salt);

                // 3. 轉成 Base64 方便儲存
                string passwordBase64 = Convert.ToBase64String(hashedPassword);
                string saltBase64 = Convert.ToBase64String(salt);

                // 4. 建立新使用者並儲存
                var newUser = new Employee
                {
                    Account = registerDto.Account,
                    Password = passwordBase64,
                    Salt = saltBase64,
                    Name = registerDto.Name,
                    Phone = registerDto.Phone,
                    Email = registerDto.Email,
                    CreateDate = DateTime.Now,
                    Identity = (IdentityEnum)registerDto.Identity
                };
                db.Employee.Add(newUser);
                db.SaveChanges();

                return Ok(new { success = true, message = "註冊成功" });
            }
        }

        // DTO 類別定義
        public class LoginDto
        {
            public string Account { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDto
        {
            public string Account { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int Identity { get; set; }
        }

    }

