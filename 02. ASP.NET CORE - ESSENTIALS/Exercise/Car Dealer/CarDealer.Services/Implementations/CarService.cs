namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Models.Cars;
    using Models.Parts;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> ByMake(string make)
        {
            return this.db
                .Cars
                .Where(c => string.Equals(c.Make, make, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance)
                .Select(c => new CarModel()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();
        }

        public IEnumerable<CarWithPartsModel> WithParts()
            => this.db
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarWithPartsModel()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts
                        .Select(p => new PartModel()
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                })
                .ToList();

        public IEnumerable<CarModel> All()
            => this.db
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarModel()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                });

        public void Create(
            string make, 
            string model, 
            long travelledDistance,
            IEnumerable<int> parts)
        {
            var existingPartIds = this.db
                .Parts
                .Where(p => parts.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            var car = new Car()
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            foreach (var partId in existingPartIds)
            {
                var partCar = new PartCar()
                {
                    PartId = partId,
                };

                car.Parts.Add(partCar);
            }

            this.db.Add(car);
            this.db.SaveChanges();
        }
    }
}
