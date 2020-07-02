namespace Demo.Services
{
    using System.Collections.Generic;

    public interface IYearsService
    {
        IEnumerable<int> GetLastYears(int count);
    }
}
