namespace BackEnd.Data
{
    using System.Collections.Generic;

    public class Track : ConferenceDTO.Track
    {
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
