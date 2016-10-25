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
    public class ScheduleController : ApiController
    {

        AirlineDatabaseEntities entity = new AirlineDatabaseEntities();

        // GET api/schedule
        [HttpGet]
        public List<AirlineCodeEntity> Get(int id)
        {
            List<AirlineCodeEntity> airCodes;
            airCodes = entity.AirPlanes.Select(x => new AirlineCodeEntity { 
                AirplaneID=x.AirplaneID, AirplaneCode=x.AirplaneCode 
            }).ToList();

            return airCodes;
        }

        // GET api/schedule/5
        [HttpGet]
        public List<ScheduleEntity> Get()
        {
            List<ScheduleEntity> sches;

            sches = (from air in entity.AirPlanes
             join sche in entity.Schedules
              on air.AirplaneID equals sche.AirplaneID
             select new ScheduleEntity
             {
                 ScheduleID=sche.ScheduleID,
                 AirplaneID = air.AirplaneID,
                 AirplaneCode = air.AirplaneCode,
                 Airport_Arrival = sche.Airport_Arrival,
                 Airport_Departure = sche.Airport_Departure,
                 Time_Arrival = sche.Time_Arrival,
                 Time_Departure = sche.Time_Departure,
                 Seats_FirstClass = air.Seats_FirstClass,
                 Seats_Economy = air.Seats_Economy,
                 Price_Economy = air.Price_Economy,
                 Price_FirstClass = air.Price_FirstClass
             }).ToList();

            return sches;
        }

        // POST api/schedule
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string value)
        {
            try
            {
                //add schedule
                ScheduleEntity scheEntity = (ScheduleEntity)JsonConvert.DeserializeObject<ScheduleEntity>(value);
                Schedule sche = new Schedule();
                sche.AirplaneID = scheEntity.AirplaneID;
                sche.Airport_Arrival = scheEntity.Airport_Arrival.ToString();
                sche.Airport_Departure = scheEntity.Airport_Departure.ToString();
                sche.Time_Arrival = scheEntity.Time_Arrival;
                sche.Time_Departure = scheEntity.Time_Departure;
                entity.Schedules.Add(sche);
                entity.SaveChanges();

                sche.ScheduleID = (int)entity.Schedules.OrderByDescending(x => x.ScheduleID).Select(x => x.ScheduleID).First();
                AirplaneEntity airplaneDetails = entity.AirPlanes.Where(x => x.AirplaneID == sche.AirplaneID).Select(x => new AirplaneEntity
                {
                    Seats_Economy = x.Seats_Economy,
                    Seats_FirstClass = x.Seats_FirstClass
                }).FirstOrDefault();

                //add seats
                Seat seatDetails;
                for (int i = 0; i < airplaneDetails.Seats_Economy; i++)
                {
                    seatDetails = new Seat();
                    seatDetails.ScheduleID = sche.ScheduleID;
                    seatDetails.SeatType = "E";
                    seatDetails.CurrentStatus = 0;
                    entity.Seats.Add(seatDetails);
                }

                for (int i = 0; i < airplaneDetails.Seats_FirstClass; i++)
                {
                    seatDetails = new Seat();
                    seatDetails.ScheduleID = sche.ScheduleID;
                    seatDetails.SeatType = "F";
                    seatDetails.CurrentStatus = 0;
                    entity.Seats.Add(seatDetails);
                }
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
