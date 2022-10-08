using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Models;
using ReservationProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Controllers
{
    public class LocationController : Controller
    {
        private readonly ReservationContext _context;

        public LocationController(ReservationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Location> locations = await (from e in _context.Locations
                                                     select e).ToListAsync();

            return View(convertLocation(locations));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LocationViewModel location)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch (location.Type)
                    {
                        case 1:
                            Bar bar = new()
                            {
                                Address = location.Address,
                                Alkohols = location.Alkohols
                            };
                            _context.Add(bar);
                            await _context.SaveChangesAsync();
                            break;
                        case 2:
                            Park park = new()
                            {
                                Address = location.Address,
                                LegalProtection = location.LegalProtection
                            };
                            _context.Add(park);
                            await _context.SaveChangesAsync();
                            break;
                        case 3:
                            Restaurant restaurant = new()
                            {
                                Address = location.Address,
                                Cuisine = location.Cuisine,
                                Decoration = location.Decoration,
                                KindOfRoom = location.KindOfRoom
                            };
                            _context.Add(restaurant);
                            await _context.SaveChangesAsync();
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(location);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationID == id);

            if (id == null)
            {
                return NotFound();
            }
            List<Location> locations = new()
            {
                location
            };

            var locationsVM = convertLocation(locations);
            var events = await _context.Events.Where(x => x.LocationID == location.LocationID).ToListAsync();
            var locationVM = locationsVM.FirstOrDefault();

            //IEnumerable<Event> eventList = new List<Event>(events);
            locationVM.Events = events;

            return View(locationVM);
        }

        private IEnumerable<LocationViewModel> convertLocation(IEnumerable<Location> locations)
        {
            List<LocationViewModel> locationsResult = new();
            foreach (Location loc in locations)
            {
                if (loc is Bar)
                {
                    var bar = loc as Bar;
                    locationsResult.Add(new LocationViewModel
                    {
                        LocationID = bar.LocationID,
                        Address = bar.Address,
                        Alkohols = bar.Alkohols,
                        Type = 1
                    });
                }
                else if (loc is Park)
                {
                    var park = loc as Park;
                    locationsResult.Add(new LocationViewModel
                    {
                        LocationID = park.LocationID,
                        Address = park.Address,
                        LegalProtection = park.LegalProtection,
                        Type = 2
                    });
                }
                else if (loc is Restaurant)
                {
                    var restaurant = loc as Restaurant;
                    locationsResult.Add(new LocationViewModel
                    {
                        LocationID = restaurant.LocationID,
                        Address = restaurant.Address,
                        Cuisine = restaurant.Cuisine,
                        Decoration = restaurant.Decoration,
                        KindOfRoom = restaurant.KindOfRoom,
                        Type = 3
                    });
                }
            }
            return locationsResult;
        }
    }
}
