using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AirlineReservation.Models;
using System.Data;
using Newtonsoft.Json;

namespace AirlineReservation.Controllers
{
    public class UserController : ApiController
    {
        //entity object
        AirlineDatabaseEntities entity = new AirlineDatabaseEntities();

        // GET api/airplane
        [HttpGet]
        public List<UserEntity> Get()
        {
            List<UserEntity> users;
            users = entity.Users.Select(x => new UserEntity
            {
                UserID = x.UserID,
                Name = x.Name,
                Age = x.Age,
                Phone = x.Phone,
                Email = x.Email
            }).ToList();

            return users;
        }

       

        // POST api/airplane
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string value)
        {

            try
            {
                var obj = JsonConvert.DeserializeObject<AirPlane>(value);
                entity.AirPlanes.Add(obj);
                entity.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


    }
}
