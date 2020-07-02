namespace Demo.Services
{
    using System;
    using System.Collections.Generic;

    public class YearsService : IYearsService   
    {
        public IEnumerable<int> GetLastYears(int count)
        {
            var currentYear = DateTime.UtcNow.Year;

            for (var year = currentYear; year >= currentYear - count; year--)
            {
                yield return year;
            }
        }
    }
}
