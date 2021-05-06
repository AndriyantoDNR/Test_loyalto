using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using Test_Andri.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test_Andri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //string cnnString = "Server=localhost;port=3306;database=test;user=root;password=root";
        string cnnString = Constring.GetConnection();

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(cnnString);
        }
        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
       
        // GET api/<LoginController>/5
        [HttpGet]
        public ResponseLogin Get([FromBody] RequestLogin Request)
        {
            ResponseLogin ResLog = new ResponseLogin();
            string auth = "GAGAL LOGIN";
            List<dbPengguna> dbpengguna = new List<dbPengguna>();
            try
            {
                var conn = GetConnection();
                conn.Open();
                string query = "select * from pengguna p where puser = '@puser' and ppas = '@ppass'";
                query = query.Replace("@puser", Request.puser);
                query = query.Replace("@ppass", Request.ppass);
                var cmd = new MySqlCommand(query, conn);
                cmd.CommandTimeout = 60 * 5;
      

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dbPengguna db = new dbPengguna();
                    db.pid = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                    db.puser = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    db.ppass = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    dbpengguna.Add(db);
                }

               
                if (dbpengguna.Count > 0) {
                    var defaults = dbpengguna.FirstOrDefault();
                    ResLog.Message = "LoginSuccess";
                    ResLog.Token = defaults.pid + DateTime.Now.ToString("HH:mm") + RandomString(5);
                }
                else
                {
                    ResLog.Message = "Gagal Login";
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ResLog;
        }
        private readonly Random _random = new Random();
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        // POST api/<LoginController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
