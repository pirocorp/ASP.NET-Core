namespace BackEnd.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Http;

    using BackEnd.Data;
    using ConferenceDTO;
    using Infrastructure;

    [Route("/api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AttendeesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<AttendeeResponse>> Get(string username)
        {
            var attendee = await this
                .context
                .Attendees
                .Include(a => a.SessionsAttendees)
                .ThenInclude(sa => sa.Session)
                .SingleOrDefaultAsync(a => a.UserName == username);

            if (attendee == null)
            {
                return this.NotFound();
            }

            var result = attendee.MapAttendeeResponse();

            return result;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AttendeeResponse>> Post(ConferenceDTO.Attendee input)
        {
            // Check if the attendee already exists
            var existingAttendee = await this.context.Attendees
                .Where(a => a.UserName == input.UserName)
                .FirstOrDefaultAsync();

            if (existingAttendee != null)
            {
                return this.Conflict(input);
            }

            var attendee = new Data.Attendee
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress
            };

            this.context.Attendees.Add(attendee);
            await this.context.SaveChangesAsync();

            var result = attendee.MapAttendeeResponse();

            return this.CreatedAtAction(nameof(this.Get), new { username = result.UserName }, result);
        }

        [HttpPost("{username}/session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AttendeeResponse>> AddSession(string username, int sessionId)
        {
            var attendee = await this
                .context
                .Attendees
                .Include(a => a.SessionsAttendees)
                .ThenInclude(sa => sa.Session)
                .SingleOrDefaultAsync(a => a.UserName == username);

            if (attendee == null)
            {
                return this.NotFound();
            }

            var session = await this.context.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return this.BadRequest();
            }

            attendee.SessionsAttendees.Add(new SessionAttendee
            {
                AttendeeId = attendee.Id,
                SessionId = sessionId
            });

            await this.context.SaveChangesAsync();

            var result = attendee.MapAttendeeResponse();

            return result;
        }

        [HttpDelete("{username}/session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveSession(string username, int sessionId)
        {
            var attendee = await this
                .context
                .Attendees
                .Include(a => a.SessionsAttendees)
                .SingleOrDefaultAsync(a => a.UserName == username);

            if (attendee == null)
            {
                return this.NotFound();
            }

            var session = await this.context.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return this.BadRequest();
            }

            var sessionAttendee = attendee
                .SessionsAttendees
                .FirstOrDefault(sa => sa.SessionId == sessionId);

            attendee.SessionsAttendees.Remove(sessionAttendee);

            await this.context.SaveChangesAsync();

            return this.NoContent();
        }
    }
}