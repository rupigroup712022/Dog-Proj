using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dog_Proj.Controllers
{
    public class DogsController : ApiController
    {
        // GET api/<controller>
        public Dogs Get()
        {
            Dogs Dogs= new Dogs();
            return user.ReadDog(Email, Password);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody]Accounts dogs)
        {
            return dogs.Insert();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}