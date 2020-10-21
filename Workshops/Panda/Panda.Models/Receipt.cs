namespace Panda.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual PandaUser User { get; set; }
    }
}
