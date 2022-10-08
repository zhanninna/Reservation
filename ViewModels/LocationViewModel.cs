using ReservationProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.ViewModels
{
    public class LocationViewModel
    {
        public int LocationID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Type { get; set; }
        public string Alkohols { get; set; }
        public bool LegalProtection { get; set; }
        public string Cuisine { get; set; }
        public string Decoration { get; set; }
        public string KindOfRoom { get; set; }
        public IEnumerable<Event> Events { get; set; }

    }
}
