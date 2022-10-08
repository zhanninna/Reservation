using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; } 
        public int QuantityofPerson { get; set; }
        public string Motive { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
        public int ServiceID { get; set; }
        public Service Service { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
