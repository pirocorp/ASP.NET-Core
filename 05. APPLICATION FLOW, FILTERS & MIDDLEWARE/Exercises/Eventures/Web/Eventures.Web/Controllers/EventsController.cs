namespace Eventures.Web.Controllers
{
    using System.Globalization;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Events;

    public class EventsController : Controller   
    {
        private readonly IEventsService _eventsService;
        private const string DATE_TIME_FORMAT = "dd-MMM-yyyy HH:mm";

        public EventsController(IEventsService eventsService)
        {
            this._eventsService = eventsService;
        }

        public IActionResult All()
        {
            var events = this
                ._eventsService
                .All()
                .Select(e => new EventListingViewModel()
                {
                    Name = e.Name,
                    Place = e.Place,
                    Start = e.Start.ToString(DATE_TIME_FORMAT, CultureInfo.InvariantCulture),
                    End = e.End.ToString(DATE_TIME_FORMAT, CultureInfo.InvariantCulture)
                })
                .ToList();

            return this.View(events);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [TypeFilter(typeof(AdminActivityLoggerFilter))]
        public IActionResult Create(EventCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this._eventsService.Create(
                model.Name,
                model.Place,
                model.Start,
                model.End,
                model.TotalTickets,
                model.Price);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
