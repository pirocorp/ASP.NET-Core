namespace CameraBazaar.Web.Models.Cameras
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    using static Common.ValidationConstants.Camera;

    public class AddCameraViewModel
    {
        public CameraMake Make { get; set; }

        [Required]
        [StringLength(ModelMaxLength)]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(QuantityMin, QuantityMax)]
        public int Quantity { get; set; }

        [Range(MinShutterSpeedMin, MinShutterSpeedMax)]
        public int MinShutterSpeed { get; set; }

        [Range(MaxShutterSpeedMin, MaxShutterSpeedMax)]
        public int MaxShutterSpeed { get; set; }
        
        // ReSharper disable InconsistentNaming
        public MinISO MinISO { get; set; }

        [Range(MaxISOMin, MaxISOMax)]
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }

        [Required]
        [StringLength(VideoResolutionMaxLength)]
        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
        public string ImageUrl { get; set; }
    }
}
