using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class Restaurant : Location
    {
        public string Cuisine { get; set; }
        public string Decoration { get; set; }
        public string KindOfRoom { get; set; }
    }
}
