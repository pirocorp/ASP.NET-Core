namespace ConferenceDTO
{
    using System.Collections.Generic;

    public class SessionResponse : Session
    {
        public Track Track { get; set; }

        public List<Speaker> Speakers { get; set; } = new List<Speaker>();
    }
}
