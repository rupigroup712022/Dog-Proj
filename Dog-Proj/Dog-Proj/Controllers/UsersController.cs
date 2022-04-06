using Dog_Proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dog_Proj.Controllers
{
    public class UsersController : ApiController
    {

        //GET api/<controller>/5
        [HttpGet]
        [Route("api/Users/?day =" + UserAv.day + "& hour=" + UserAv.hour;")]
        public List<List<string>> Get(string day, string hour )
        {
            Console.WriteLine(day,hour);
            User user = new User();
            return user.GetAvUser(day,hour);
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return user.InsertUser();
        }


        
    }
}