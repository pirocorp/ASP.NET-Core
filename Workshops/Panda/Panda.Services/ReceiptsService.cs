namespace Panda.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Panda.Models;

    public class ReceiptsService : IReceiptsService
    {
        private readonly PandaDbContext pandaDb;

        public ReceiptsService(PandaDbContext pandaDb)
        {
            this.pandaDb = pandaDb;
        }

        public async Task<string> CreateAsync(ReceiptCreateServiceModel model)
        {
            var receipt = model.To<Receipt>();

            await this.pandaDb.Receipts.AddAsync(receipt);
            await this.pandaDb.SaveChangesAsync();

            return receipt.Id;
        }

        public async Task<IEnumerable<TOutput>> GetAllAsync<TOutput>()
            => await this.pandaDb.Receipts
                .To<TOutput>()
                .ToListAsync();

        public async Task<IEnumerable<TOutput>> GetAllByUserAsync<TOutput>(string userId)
            => await this.pandaDb.Receipts
                .Where(r => r.RecipientId.Equals(userId))
                .To<TOutput>()
                .ToListAsync();

        public async Task<TOutput> GetByIdAsync<TOutput>(string receiptId)
            => await this.pandaDb.Receipts
                .Where(r => r.Id.Equals(receiptId))
                .To<TOutput>()
                .FirstOrDefaultAsync();
    }
}
