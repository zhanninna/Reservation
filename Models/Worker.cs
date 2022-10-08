using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class Worker : Osoba
    {
        public int ServiceID { get; set; }
        public Service Service { get; set; }
        public float NettoPerHour { get; set; } = 19;
        public int WorkHours { get; set; }
        public DateTime BirthDate { get; set; }

        
    }
}
