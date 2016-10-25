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
    public class ReservationController : ApiController
    {
        //entity object
        AirlineDatabaseEntities entity = new AirlineDatabaseEntities();

        // GET api/reservation/5
        [HttpGet]
        public List<SeatEntity> Get(int id)
        {
            List<SeatEntity> seats = entity.Seats.Where(x => x.ScheduleID == id).Select(x => new SeatEntity
            {
                ScheduleID = (int)x.ScheduleID,
                SeatID = x.SeatID,
                SeatType = x.SeatType,
                CurrentStatus = (int)x.CurrentStatus
            }).ToList();

            return seats;
        }

        // POST api/reservation
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string value)
        {
            SeatEntity seat = JsonConvert.DeserializeObject<SeatEntity>(value);

            Seat updateSeat = entity.Seats.Where(x => x.SeatID == seat.SeatID).First();
            //only reserve if seat is still available
            if (updateSeat.CurrentStatus == 1 || updateSeat.CurrentStatus == 0)
            {
                //add user record
                AirlineReservation.Models.User user = new User();
                user.Name = seat.Name;
                user.Age = seat.Age;
                user.Email = seat.Email;
                entity.Users.Add(user);
                entity.SaveChanges();

                int userID = entity.Users.OrderByDescending(x => x.UserID).Select(x => x.UserID).First();

                //Modify seats record
                entity.Seats.Attach(updateSeat);
                updateSeat.UserID = userID;
                updateSeat.CurrentStatus = 2;
                entity.Entry(updateSeat).State = EntityState.Modified;
                entity.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
