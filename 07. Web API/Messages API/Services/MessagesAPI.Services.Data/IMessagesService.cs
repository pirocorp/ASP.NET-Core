namespace MessagesAPI.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MessagesAPI.Data.Models;
    using Models;

    public interface IMessagesService
    {
        Task<Message> CreateAsync(string content, string user);

        Task<IEnumerable<MessageServiceListingModel>> AllAsync(int? count);
    }
}
