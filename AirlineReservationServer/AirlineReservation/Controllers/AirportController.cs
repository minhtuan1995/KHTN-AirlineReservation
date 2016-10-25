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
    public class AirportController : ApiController
    {

        //entity object
        AirlineDatabaseEntities entity = new AirlineDatabaseEntities();

        // GET api/airplane
        [HttpGet]
        public List<AirportEntity> Get()
        {
            List<AirportEntity> airports;
            airports = entity.AirPorts.Select(x => new AirportEntity
            {
                AirportID = x.AirportID,
                AirportName = x.AirportName               
            }).ToList();
            return airports;
        }
    }
}
