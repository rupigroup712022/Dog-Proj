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
        // GET api/<controller>
        public User Get(string Email, string Password)
        {
            User user = new User();
            return user.ReadUser(Email, Password);
        }


        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public int Post([FromBody] User user)
        {
            return user.Insert();
        }
    }