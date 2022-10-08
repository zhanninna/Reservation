using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public abstract class Osoba
    {
        public int OsobaID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfPhone { get; set; }
        public string Email { get; set; }

    }
}
