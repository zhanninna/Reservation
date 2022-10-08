using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public string StatusOfReservation { get; set; }
        public DateTime TheDateOfMakeReserve { get; set; }
        public DateTime TheLastPaymentsDay { get; set; }
        public int ReservationPeriodinDays { get; set; }
        public float CostPerDay { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

       
    }
}
