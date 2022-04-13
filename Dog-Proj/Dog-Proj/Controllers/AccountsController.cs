using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dog_Proj.Models;

namespace Dog_Proj.Controllers
{
    public class AccountsController : ApiController
    {
     
        // GET api/<controller>
        public Account Get(string email, string password)
        {
            Account accounts = new Account();
            return accounts.ReadAccount(email, password);
        }
        //[HttpGet]
        //[Route("api/Accounts/User")]
        public List<string> Get(int Id)
        {
            Account account = new Account();
            return account.GetUserSelction(Id);
        }


        // POST api/<controller>
        public int Post([FromBody]Account account)
        {
            return account.Insert();
        }

        [HttpPost]
        [Route("api/Accounts/address")]
        public List<string> GetAddress([FromBody] Dictionary<string, int> familyId)
        {
            Account account = new Account();
            return account.GetAddress(familyId["familyId"]);
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