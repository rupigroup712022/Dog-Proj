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
            return user.GetAvUser(userAv["day"], userAv["hour"],Convert.ToInt32(userAv["userid"]), 
                Convert.ToInt16(userAv["serviceId"]));
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return user.InsertUser();
        }

        [HttpPost]
        [Route("api/Users/incomePendingReq")]

        public List<List<string>> getIncomePendingRequests([FromBody] Dictionary<string, string> u)//בקשות נכנסות
        {
            User user = new User();
            return user.getIncomePendingRequests(Convert.ToInt32(u["userId"]));
        }

        [HttpPost]
        [Route("api/Users/CountRequestsApro")]

        public int CountRequestsApro([FromBody] Dictionary<string, string> u)
        {
            User user = new User();
            return user.CountRequestsApro(Convert.ToInt32(u["userId"]));
        }

        [HttpPost]
        [Route("api/Users/incomeApprovedReq")]
        public List<List<string>> getIncomeApprovedRequests([FromBody] Dictionary<string, string> u)//בקשות נכנסות
        {
            User user = new User();
            return user.getIncomeApprovedRequests(Convert.ToInt32(u["userId"]));
        }

        [HttpPost]
        [Route("api/Users/getRequestHistory")]
        public List<List<string>> getRequestHistory([FromBody] Dictionary<string, string> u)
        {
            User user = new User();
            return user.getRequestHistory(Convert.ToInt32(u["userId"]));
        }

        [HttpPost]
        [Route("api/Users/getWaitResponse")]
        public List<List<string>> getWaitResponse([FromBody] Dictionary<string, string> u)
        {
            User user = new User();
            return user.getWaitResponse(Convert.ToInt32(u["userId"]));
        }
        [HttpPost]
        [Route("api/Users/CountWaitResponse")]
        public int CountWaitResponse([FromBody] Dictionary<string, string> u)
        {
            User user = new User();
            return user.CountWaitResponse(Convert.ToInt32(u["userId"]));
        }




        [HttpPost]
        [Route("api/Users/getWaitApproval")]
        public List<List<string>> getWaitApproval([FromBody] Dictionary<string, string> u)
        {
            User user = new User();
            return user.getWaitApproval(Convert.ToInt32(u["userId"]));
        }

        [HttpGet]
        [Route("api/Users/outcomeReq")]
        public int GetOutcome([FromBody] User user)//בקשות יוצאות
        {
            return user.InsertUser();
        }

        [HttpPost]
        [Route("api/Users/answeredRequest")]
        public string answeredRequest([FromBody] Dictionary<string, string> u)
        {
            bool b;
            User user = new User();
            if (u["val"]=="true")
            {
                b = true; 
            }
            else
            {
                b = false;
            }
             user.setRequests(Convert.ToInt32(u["userId"]),u["serviceId"],b, u["type"]);
            return u["serviceId"];
        }



        [HttpPost]
        [Route("api/Users/setSelfCensel")]
        public string setSelfCensel([FromBody] Dictionary<string, string> u)
        {
            User user = new User();
            user.setSelfCensel(Convert.ToInt32(u["userId"]), u["serviceId"], u["type"]);
            return u["serviceId"];
        }

        [HttpPost]
        [Route("api/Users/userAvPension")]
        public List<List<string>> PostPension([FromBody] Dictionary<string, string> userAv)
        {
            User user = new User();
            return user.GetAvUserPension(Convert.ToInt32(userAv["userid"]), Convert.ToInt16(userAv["serviceId"]));
        }

    }
}

