using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.Models
{
    public class ScheduleEntity
    {
        public int AirplaneID { get; set; }
        public string AirplaneCode { get; set; }
        public int? Seats_FirstClass { get; set; }
        public int? Seats_Economy { get; set; }
        public double? Price_FirstClass { get; set; }
        public double? Price_Economy { get; set; }
        public int ScheduleID { get; set; }
        public string Airport_Departure { get; set; }
        public string Airport_Arrival { get; set; }
        public DateTime? Time_Departure { get; set; }
        public DateTime? Time_Arrival { get; set; }
    }

    public class AirplaneEntity
    {
        public int AirplaneID { get; set; }
        public string AirplaneCode { get; set; }
        public int? Seats_FirstClass { get; set; }
        public int? Seats_Economy { get; set; }
        public double? Price_FirstClass { get; set; }
        public double? Price_Economy { get; set; }
    }

    public class AirlineCodeEntity
    {
        public int AirplaneID { get; set; }
        public string AirplaneCode { get; set; }
    }

    public class SeatEntity
    {
        public int ScheduleID { get; set; }
        public int SeatID { get; set; }
        public string SeatType { get; set; }
        public int CurrentStatus { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class UserEntity
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

}