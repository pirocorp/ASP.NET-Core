namespace Panda.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual PandaUser User { get; set; }
    }
}
