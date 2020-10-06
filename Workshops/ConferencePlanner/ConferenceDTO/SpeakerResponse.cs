namespace ConferenceDTO
{
    using System.Collections.Generic;

    public class SpeakerResponse : Speaker
    {
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
