﻿using System;
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
        public Account Get(string Email, string Password)
        {
            Account accounts = new Account();
            return accounts.ReadAccount(Email, Password);
        }
        //[HttpGet]
        //[Route("api/Accounts/User")]
        public List<string> Get(int Id)
        {
            Account account = new Account();
            return account.GetUserSelction(Id);
        }


        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public int Post([FromBody]Account account)
        {
            return account.Insert();
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