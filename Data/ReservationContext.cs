using Microsoft.EntityFrameworkCore;
using ReservationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Data
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
        }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Klients> Klients { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Osoba> Osobas { get; set; }
        public DbSet<Park> Parks { get; set; }
        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }
        public DbSet<PrivateEvent> PrivateEvents { get; set; }
        public DbSet<PublicEvent> PublicEvents { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Osoba>().ToTable("People");
        }
    }
}
