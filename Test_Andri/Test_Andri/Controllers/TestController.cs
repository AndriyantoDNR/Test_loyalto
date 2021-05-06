using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Test_Andri.Model;

namespace Test_Andri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        string cnnString = "Server=localhost;Initial Catalog=test;Persist Security Info=False;User ID=root;Password=root;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=3600;";

        [HttpGet]
        public ResponseLogin Login(RequestLogin Request)
        {
            ResponseLogin responseLogin = new ResponseLogin();
            try
            {
                GetAuth(Request);
            }
            catch (Exception)
            {

                throw;
            }
            return responseLogin;
        }

        public string GetAuth(RequestLogin Request)
        {
            string auth = "GAGAL LOGIN";
            try
            {
                var conn = new NpgsqlConnection(cnnString);
                conn.Open();
                string query = "select * from pengguna p where puser = '[1]' and ppas = '[2]'";
                query.Replace("[1]", Request.puser);
                query.Replace("[2]", Request.ppass);
                var cmd = new NpgsqlCommand(query, conn);
                cmd.CommandTimeout = 60 * 5;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return auth;
        }
    }
}
