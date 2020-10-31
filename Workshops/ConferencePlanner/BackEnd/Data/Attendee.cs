namespace BackEnd.Data
{
    using System.Collections.Generic;

    public class Attendee : ConferenceDTO.Attendee
    {
        public virtual ICollection<SessionAttendee> SessionsAttendees { get; set; }
    }
}
