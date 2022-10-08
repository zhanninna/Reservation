using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Models
{
    public class LegalPerson : Klients
    {
        public string NameOfCompany { get; set; }
        public float Iban { get; set; }
        public float Budget { get; set; }

    }
}
