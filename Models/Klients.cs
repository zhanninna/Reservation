using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class Klients : Osoba
    {
        public int Discount { get; set; } = 50;
    }
}
