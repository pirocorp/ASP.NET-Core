namespace BackEnd.Data
{
    using System.IO;
    using System.Threading.Tasks;

    public abstract class DataLoader
    {
        public abstract Task LoadDataAsync(Stream fileStream, ApplicationDbContext db);
    }

}