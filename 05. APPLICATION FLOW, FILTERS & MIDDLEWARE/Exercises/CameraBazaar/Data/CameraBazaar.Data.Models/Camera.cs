﻿namespace CameraBazaar.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ValidationConstants.Camera;
    public class Camera
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
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
        [MaxLength(VideoResolutionMaxLength)]
        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(ImageUrlMinLength)]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
