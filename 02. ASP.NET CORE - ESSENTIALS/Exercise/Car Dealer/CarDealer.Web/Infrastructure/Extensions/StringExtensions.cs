namespace CarDealer.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToPrice(this decimal number)
            => $"${number:F2}";

        public static string ToPercent(this double number)
            => $"{number * 100}%";

        public static string ToKm(this long number)
            => $"{(number / 1000):N0} KM";
    }
}
