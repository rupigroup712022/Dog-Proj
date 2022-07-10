using Dog_Proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dog_Proj.Controllers
{
    public class ServicesController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public int Post(int UserId,[FromBody] Service service)
        {
            return service.InsertServices(UserId);
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        [Route ("api/Services/req")]
        public int Post([FromBody] Dictionary<string, int> req)
        {
            Service service = new Service();  
            return service.InsertReqServices(req["idService"], req["idUser"],req["reqUser"],req["servicetype"]);
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/Services/setRating")]
        public string setRating([FromBody] Dictionary<string, string> u)
        {
            Service service = new Service();
            service.setRating(Convert.ToInt16(u["service_id"]), Convert.ToInt16(u["rating"]), Convert.ToInt16(u["handlerId"]),
                Convert.ToInt32(u["type"]));
            return u["service_id"];
        }
    }
}