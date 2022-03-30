using Dog_Proj.Models;
using DOGS1.Models;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return user.InsertUser();
        }
    }
}