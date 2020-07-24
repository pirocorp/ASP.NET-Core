namespace CameraBazaar.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public void Create(
            CameraMake make, 
            string model, 
            decimal price, 
            int quantity, 
            int minShutterSpeed, 
            int maxShutterSpeed,
            // ReSharper disable InconsistentNaming
            MinISO minISO, 
            int maxISO, 
            bool isFullFrame, 
            string videoResolution, 
            IEnumerable<LightMetering> lightMetering,
            string description, 
            string imageUrl,
            string userId)
        {
            var camera = new Camera()
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMetering.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }
    }
}
