namespace BackEnd.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using ConferenceDTO;
    using Data;
    using Infrastructure;

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SpeakersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Speakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerResponse>>> GetSpeakers()
        {
            var speakers = await this.context
                .Speakers
                .AsNoTracking()
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .Select(s => s.MapSpeakerResponse())
                .ToListAsync();

            return speakers;
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpeakerResponse>> GetSpeaker(int id)
        {
            var speaker = await this.context
                .Speakers
                .AsNoTracking()
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .SingleOrDefaultAsync(s => s.Id == id);

            if (speaker == null)
            {
                return this.NotFound();
            }

            return speaker.MapSpeakerResponse();
        }
    }
}
