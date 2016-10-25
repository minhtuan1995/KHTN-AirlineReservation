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
    [AllowAnonymous]
    public class AirPlaneController : ApiController
    {
        
        //entity object
        AirlineDatabaseEntities entity = new AirlineDatabaseEntities();

        // GET api/airplane
        [HttpGet]
        public List<AirplaneEntity> Get()
        {
            List<AirplaneEntity> airplanes;
            airplanes = entity.AirPlanes.Select(x => new AirplaneEntity
            {
                AirplaneID = x.AirplaneID,
                AirplaneCode = x.AirplaneCode,
                Price_Economy = x.Price_Economy,
                Price_FirstClass = x.Price_FirstClass,
                Seats_Economy = x.Seats_Economy,
                Seats_FirstClass = x.Seats_FirstClass
            }).ToList();

            return airplanes;
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
