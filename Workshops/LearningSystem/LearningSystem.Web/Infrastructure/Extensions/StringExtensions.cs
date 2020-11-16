namespace LearningSystem.Web.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
            => Regex.Replace(text, @"[^A-Za-zа-яА-Я0-9_\.~]+", "-")
                .Trim('-')
                .ToLower();
    }
}
