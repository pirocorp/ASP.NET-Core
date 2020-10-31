namespace BackEnd.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using BackEnd.Data;
    using ConferenceDTO;
    using Infrastructure;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext context;

        public SessionsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SessionResponse>>> Get()
        {
            var sessions = await this
                .context
                .Sessions
                .AsNoTracking()
                .Include(s => s.Track)
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Speaker)
                .Select(m => m.MapSessionResponse())
                .ToListAsync();

            return sessions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponse>> Get(int id)
        {
            var session = await this
                .context
                .Sessions
                .AsNoTracking()
                .Include(s => s.Track)
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Speaker)
                .SingleOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return this.NotFound();
            }

            return session.MapSessionResponse();
        }

        [HttpPost]
        public async Task<ActionResult<SessionResponse>> Post(ConferenceDTO.Session input)
        {
            var session = new Data.Session
            {
                Title = input.Title,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                Abstract = input.Abstract,
                TrackId = input.TrackId
            };

            await this.context.Sessions.AddAsync(session);
            await this.context.SaveChangesAsync();

            var result = session.MapSessionResponse();

            return this.CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ConferenceDTO.Session input)
        {
            var session = await this.context.Sessions.FindAsync(id);

            if (session == null)
            {
                return this.NotFound();
            }

            session.Id = input.Id;
            session.Title = input.Title;
            session.Abstract = input.Abstract;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;
            session.TrackId = input.TrackId;

            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SessionResponse>> Delete(int id)
        {
            var session = await this.context.Sessions.FindAsync(id);

            if (session == null)
            {
                return this.NotFound();
            }

            this.context.Sessions.Remove(session);
            await this.context.SaveChangesAsync();

            return session.MapSessionResponse();
        }


        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm]ConferenceFormat format, IFormFile file)
        {
            var loader = GetLoader(format);

            await using (var stream = file.OpenReadStream())
            {
                await loader.LoadDataAsync(stream, this.context);
            }

            await this.context.SaveChangesAsync();

            return this.Ok();
        }

        private static DataLoader GetLoader(ConferenceFormat format)
        {
            if (format == ConferenceFormat.Sessionize)
            {
                return new SessionizeLoader();
            }
            return new DevIntersectionLoader();
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum ConferenceFormat
        {
            Sessionize,
            DevIntersections
        }
    }
}