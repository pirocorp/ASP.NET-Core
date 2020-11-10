namespace JokesApp.Services.Models.Home
{
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class IndexJokeViewModel : IMapFrom<Joke>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration
                .CreateMap<Joke, IndexJokeViewModel>()
                .ForMember(
                    j => j.CategoryName,
                    opt => opt.MapFrom(i => $"{i.Category.Name} ({i.Category.Jokes.Count})"));

    }
}
