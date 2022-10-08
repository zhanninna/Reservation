using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string TeamName { get; set; }
        public string PositionWorker { get; set; }
        public string Competencies { get; set; }
        public ICollection<Worker> Workers { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
