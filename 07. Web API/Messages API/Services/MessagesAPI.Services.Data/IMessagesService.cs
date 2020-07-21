namespace MessagesAPI.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MessagesAPI.Data.Models;

    public interface IMessagesService
    {
        Task<Message> CreateAsync(string content, string user);

        Task<IEnumerable<Message>> AllAsync();
    }
}
