namespace CameraBazaar.Data.Models.Extensions
{
    using Models;

    public static class EnumExtensions
    {
        public static string ToFriendlyName(this LightMetering lightMetering)
        {
            if (lightMetering == LightMetering.CenterWeighted)
            {
                return "Center-Weighted";
            }

            return lightMetering.ToString();
        }
    }
}
