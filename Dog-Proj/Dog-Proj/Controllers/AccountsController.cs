using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dog_Proj.Controllers
{
    public class AccountsController : ApiController
    {
     
        // GET api/<controller>
        public Accounts Get(string Email, string Password)
        {
            Accounts Accounts = new Accounts();
            return user.ReadUser(Email, Password);
        }


        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public int Post([FromBody]Accounts accounts)
        {
            return accounts.Insert();
        }

        //public int Put(int id)
        //{
        //    Accounts accounts = new Accounts();
        //    return accounts.UpdateAccounts(id);
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //    Accounts accounts = new Accounts();
        //    accounts.DeleteAccounts(id);
        //}
    }
}