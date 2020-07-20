namespace Eventures.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Models.Events;

    public class EventsService : IEventsService
    {
        private readonly EventuresDbContext db;

        public EventsService(EventuresDbContext db)
        {
            this.db = db;
        }

        public void Create(
            string name, 
            string place, 
            DateTime start, 
            DateTime end, 
            int totalTickets, 
            decimal price)
        {
            var @event = new Event()
            {
                Name = name,
                Place = place,
                Start = start,
                End = end,
                TotalTickets = totalTickets,
                Price = price
            };

            this.db.Events.Add(@event);
            this.db.SaveChanges();
        }

        public IEnumerable<EventListingModel> All()
            => this.db
                .Events
                .Select(e => new EventListingModel()
                {
                    Name = e.Name,
                    Place = e.Place,
                    Start = e.Start,
                    End = e.End
                })
                .ToList();
    }
}
