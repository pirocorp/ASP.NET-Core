namespace BookLibrary.Web.Extensions
{
    using System;
    using Data.Models;

    public static class EnumExtensions
    {
        public static string FriendlyToString(this BookStatus status) =>
            status switch
            {
                BookStatus.AtHome => "At home",
                BookStatus.Borrowed => status.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };

        public static string FriendlyToColor(this BookStatus status) =>
            status switch
            {
                BookStatus.AtHome => "text-success",
                BookStatus.Borrowed => "text-danger",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
    }
}
