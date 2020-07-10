namespace CameraBazaar.Web.Models.Cameras
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    using static Common.ValidationConstants.Camera;
    using static Common.DisplayConstants.Camera;

    public class AddCameraViewModel
    {
        [Display(Name = MakeDisplay)]
        public CameraMake Make { get; set; }

        [Required]
        [StringLength(ModelMaxLength)]
        [Display(Name = ModelDisplay)]
        public string Model { get; set; }

        [Display(Name = PriceDisplay)]
        public decimal Price { get; set; }

        [Range(QuantityMin, QuantityMax)]
        [Display(Name = QuantityDisplay)]
        public int Quantity { get; set; }

        [Range(MinShutterSpeedMin, MinShutterSpeedMax)]
        [Display(Name = MinShutterSpeedDisplay)]
        public int MinShutterSpeed { get; set; }

        [Range(MaxShutterSpeedMin, MaxShutterSpeedMax)]
        [Display(Name = MaxShutterSpeedDisplay)]
        public int MaxShutterSpeed { get; set; }
        
        // ReSharper disable InconsistentNaming
        [Display(Name = MinISODisplay)]
        public MinISO MinISO { get; set; }

        [Range(MaxISOMin, MaxISOMax)]
        [Display(Name = MaxISODisplay)]
        public int MaxISO { get; set; }

        [Display(Name = IsFullFrameDisplay)]
        public bool IsFullFrame { get; set; }

        [Required]
        [StringLength(VideoResolutionMaxLength)]
        [Display(Name = VideoResolutionDisplay)]
        public string VideoResolution { get; set; }

        [Display(Name = LightMeteringDisplay)]
        public IEnumerable<LightMetering> LightMetering { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        [Display(Name = DescriptionDisplay)]
        public string Description { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
        [Display(Name = ImageUrlDisplay)]
        public string ImageUrl { get; set; }
    }
}
