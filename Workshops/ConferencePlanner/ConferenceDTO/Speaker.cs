namespace ConferenceDTO
{
    using System.ComponentModel.DataAnnotations;

    public class Speaker
    {
        public int Id { get; set; }

        /// <summary>
        /// The name of the speaker
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Biographical information about speaker
        /// </summary>
        [StringLength(4000)]
        public string Bio { get; set; }

        /// <summary>
        /// More information
        /// </summary>
        [StringLength(1000)]
        public virtual string WebSite { get; set; }
    }
}
