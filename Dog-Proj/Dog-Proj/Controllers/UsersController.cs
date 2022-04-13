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

        [HttpPost]
        [Route("api/Users/userAv")]

        public List<List<string>> Post([FromBody]Dictionary<string, string> userAv)
        {
            User user = new User();
            return user.GetAvUser(userAv["day"], userAv["hour"]);
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return user.InsertUser();
        }


    }
}