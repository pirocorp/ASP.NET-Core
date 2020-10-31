namespace ConferenceDTO
{
    using System.Collections.Generic;

    public class AttendeeResponse : Attendee
    {
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
