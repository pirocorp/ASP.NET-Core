namespace Eventures.Services
{
    using System;
    using System.Collections.Generic;
    using Models.Events;

    public interface IEventsService
    {
        void Create(
            string name,
            string place,
            DateTime start,
            DateTime end,
            int totalTickets,
            decimal price);
        
        IEnumerable<EventListingModel> All();
    }
}
