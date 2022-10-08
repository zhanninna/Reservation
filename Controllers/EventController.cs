using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class EventController : Controller
    {
        private readonly ReservationContext _context;

        public EventController(ReservationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["QuantitySortParm"] = sortOrder == "QuantityofPerson" ? "quantity_desc" : "QuantityofPerson";

            IEnumerable<Event> events = await(from e in _context.Events
                                                    select e).ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            events = sortOrder switch
            {
                "name_desc" => events.OrderByDescending(s => s.Name),
                "QuantityofPerson" => events.OrderBy(s => s.QuantityofPerson),
                "quantity_desc" => events.OrderByDescending(s => s.QuantityofPerson),
                _ => events.OrderBy(s => s.Name),
            };
            return View(events);
        }

        public async Task<IActionResult> Register()
        {
            EventViewModel eventVM = new();
            IEnumerable<Location> locations = await (from e in _context.Locations
                                                    select e).ToListAsync();
            IEnumerable<Service> services = await (from e in _context.Services
                                                     select e).ToListAsync();
            List<SelectListItem> locationsSelectList = new();
            foreach(var loc in locations)
            {
                locationsSelectList.Add(new SelectListItem { Text = loc.Address, Value = loc.LocationID.ToString() });
            }

            List<SelectListItem> servicesSelectList = new();
            foreach (var ser in services)
            {
                servicesSelectList.Add(new SelectListItem { Text = ser.TeamName, Value = ser.ServiceID.ToString() });
            }

            eventVM.Locations = locationsSelectList;
            eventVM.Services = servicesSelectList;

            return View(eventVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(EventViewModel eventVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch (eventVM.Type)
                    {
                        case 1:
                            PrivateEvent privateEvent = new()
                            {
                                Name = eventVM.Name,
                                Motive = eventVM.Motive,
                                QuantityofPerson = eventVM.QuantityofPerson,
                                Description = eventVM.Description,
                                LocationID = eventVM.LocationID,
                                ServiceID = eventVM.ServiceID
                            };
                            _context.Add(privateEvent);
                            await _context.SaveChangesAsync();
                            break;
                        case 2:
                            PublicEvent publicEvent = new PublicEvent
                            {
                                Name = eventVM.Name,
                                Motive = eventVM.Motive,
                                QuantityofPerson = eventVM.QuantityofPerson,
                                ArtShow = eventVM.ArtShow,
                                LocationID = eventVM.LocationID,
                                ServiceID = eventVM.ServiceID
                            };
                            _context.Add(publicEvent);
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
            return View(eventVM);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var ev = await _context.Events
                .FirstOrDefaultAsync(m => m.EventID == id);

            var loc = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationID == ev.LocationID);

            var team = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceID == ev.ServiceID);

            EventViewModel eventVM = new();
            eventVM.Location = loc;
            eventVM.Service = team;

            if (ev is PrivateEvent)
            {
                var privateEvent = ev as PrivateEvent;
                eventVM.EventID = privateEvent.EventID;
                eventVM.Type = 1;
                eventVM.Description = privateEvent.Description;
                eventVM.Name = privateEvent.Name;
                eventVM.Motive = privateEvent.Motive;
                eventVM.QuantityofPerson = privateEvent.QuantityofPerson;
            }
            else
            {
                var publicEvent = ev as PublicEvent;
                eventVM.EventID = publicEvent.EventID;
                eventVM.Type = 2;
                eventVM.ArtShow = publicEvent.ArtShow;
                eventVM.Name = publicEvent.Name;
                eventVM.Motive = publicEvent.Motive;
                eventVM.QuantityofPerson = publicEvent.QuantityofPerson;
            }

            if (id == null)
            {
                return NotFound();
            }


            return View(eventVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
            {
                return NotFound();
            }

            EventViewModel eventVM = new();
            IEnumerable<Location> locations = await (from e in _context.Locations
                                                     select e).ToListAsync();
            IEnumerable<Service> services = await (from e in _context.Services
                                                   select e).ToListAsync();
            List<SelectListItem> locationsSelectList = new();
            foreach (var loc in locations)
            {
                locationsSelectList.Add(new SelectListItem { Text = loc.Address, Value = loc.LocationID.ToString() });
            }

            List<SelectListItem> servicesSelectList = new();
            foreach (var ser in services)
            {
                servicesSelectList.Add(new SelectListItem { Text = ser.TeamName, Value = ser.ServiceID.ToString() });
            }

            eventVM.Locations = locationsSelectList;
            eventVM.Services = servicesSelectList;
            eventVM.Name = ev.Name;
            eventVM.QuantityofPerson = ev.QuantityofPerson;
            eventVM.Motive = ev.Motive;

            return View(eventVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventViewModel eventVM)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        switch (eventVM.Type)
                        {
                            case 1:
                                PrivateEvent privateEvent = new()
                                {
                                    EventID = id,
                                    Name = eventVM.Name,
                                    Motive = eventVM.Motive,
                                    QuantityofPerson = eventVM.QuantityofPerson,
                                    Description = eventVM.Description,
                                    LocationID = eventVM.LocationID,
                                    ServiceID = eventVM.ServiceID
                                };
                                _context.Update(privateEvent);
                                await _context.SaveChangesAsync();
                                break;
                            case 2:
                                PublicEvent publicEvent = new()
                                {
                                    EventID = id,
                                    Name = eventVM.Name,
                                    Motive = eventVM.Motive,
                                    QuantityofPerson = eventVM.QuantityofPerson,
                                    ArtShow = eventVM.ArtShow,
                                    LocationID = eventVM.LocationID,
                                    ServiceID = eventVM.ServiceID
                                };
                                _context.Update(publicEvent);
                                await _context.SaveChangesAsync();
                                break;

                            default:
                                throw new InvalidOperationException();
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        var eventToUpdate = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);
                        if (eventToUpdate == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ev = await _context.Events
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, Event eventToDelete)
        {
            var checkEvent = await _context.Events.FindAsync(eventToDelete.EventID);
            _context.Events.Remove(checkEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
