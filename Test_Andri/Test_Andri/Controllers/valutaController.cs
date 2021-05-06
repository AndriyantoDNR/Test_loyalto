using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Test_Andri.Attributes;
using Test_Andri.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test_Andri.Controllers
{

    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class valutaController : ControllerBase
    {
        // GET: api/<valutaController>
        //string cnnString = "Server=localhost;port=3306;database=test;user=root;password=root";
        string cnnString = Constring.GetConnection();
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(cnnString);
        }

        [HttpGet]
        public List<dbvaluta> Get()
        {
            List<dbvaluta> Resvaluta = new List<dbvaluta>();
            var conn = GetConnection();
            conn.Open();
            string query = "select * from valuta p";
         
            var cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 60 * 5;


            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dbvaluta db = new dbvaluta();
                db.vkode = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                db.vname = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                db.vprice = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                Resvaluta.Add(db);
            }

            return Resvaluta;

        }

        // GET api/<valutaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<valutaController>
        [HttpPost]
        public void Post([FromBody] dbvaluta Response)
        {
            var conn = GetConnection();
            conn.Open();
            string query = "INSERT INTO test.valuta(vkode, vname, vprice) VALUES('@vkode', '@vname', '@vprice'); ";
            query = query.Replace("@vkode", Response.vkode);
            query = query.Replace("@vname", Response.vname);
            query = query.Replace("@vprice", Response.vprice);
            var cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 60 * 5;
            var reader = cmd.ExecuteReader();
        }

        // PUT api/<valutaController>/5
        [HttpPut]
        public void Put(dbvaluta Response)
        {
            var conn = GetConnection();
            conn.Open();
            string query = "UPDATE test.valuta SET vname = '@VNAME', vprice = '@VPRICE' WHERE vKode = '@VKODE';            ";
            query = query.Replace("@VKODE", Response.vkode);
            query = query.Replace("@VNAME", Response.vname);
            query = query.Replace("@VPRICE", Response.vprice);
            var cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 60 * 5;
            var reader = cmd.ExecuteReader();
        }

        // DELETE api/<valutaController>/5
        [HttpDelete]
        public void Delete(dbvaluta Response)
        {
            var conn = GetConnection();
            conn.Open();
            string query = "delete FROM test.valuta WHERE vKode = '@VKODE';            ";
            query = query.Replace("@VKODE", Response.vkode);
     
            var cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 60 * 5;
            var reader = cmd.ExecuteReader();
        }
    }
}
