namespace Stopify.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VoteCreateServiceModel
    {
        public string UserId { get; set; }

        public string ProductId { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }
    }
}
