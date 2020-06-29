namespace CarDealer.Web.ViewModels.Parts
{
    using System;
    using Services.Models.Parts;
    using System.Collections.Generic;

    public class PartPageListingModel
    {
        public IEnumerable<PartListingModel> Parts { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => Math.Max(1, this.CurrentPage - 1);

        public int NextPage => Math.Min(this.TotalPages, this.CurrentPage + 1);
    }
}
