using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public abstract class Location
    {
        public int LocationID { get; set; }
        public string Address { get; set; }

        public ICollection<Event> Events { get; set; }

    }
}
