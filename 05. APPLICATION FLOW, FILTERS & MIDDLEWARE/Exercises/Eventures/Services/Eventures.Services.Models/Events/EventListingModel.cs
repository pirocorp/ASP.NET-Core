namespace Eventures.Services.Models.Events
{
    using System;

    public class EventListingModel
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
