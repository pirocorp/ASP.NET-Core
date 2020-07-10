namespace CameraBazaar.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Flags]
    public enum LightMetering
    {
        None = 0,
        Spot = 1,
        [Display(Name = "Center Weighted")]
        CenterWeighted = 2,
        Evaluative = 4,
    }
}