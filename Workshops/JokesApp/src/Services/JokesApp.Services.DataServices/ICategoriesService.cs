namespace JokesApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICategoriesService
    {
        Task<IEnumerable<DropDownViewModel>> GetAllAsync(); 

        bool Exists(int categoryId);
    }
}
