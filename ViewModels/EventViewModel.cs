using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.ViewModels
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public int QuantityofPerson { get; set; }
        public string Motive { get; set; }
        public int LocationID { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public int ServiceID { get; set; }
        public IEnumerable<SelectListItem> Services { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string ArtShow { get; set; }
        public Location Location { get; set; }
        public Service Service { get; set; }

    }
}
