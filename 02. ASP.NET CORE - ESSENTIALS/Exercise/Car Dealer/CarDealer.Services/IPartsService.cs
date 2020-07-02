namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models.Parts;

    public interface IPartsService
    {
        IEnumerable<PartListingModel> AllListings(int page = 1, int pageSize = 10);

        IEnumerable<PartBasicModel> All();

        void Create(string modelName, decimal modelPrice, int modelQuantity, int modelSupplierId);
        
        int Count();

        void Delete(int id);

        PartDetailsModel ById(int id);

        void Edit(int id, decimal price, int quantity);
    }
}
