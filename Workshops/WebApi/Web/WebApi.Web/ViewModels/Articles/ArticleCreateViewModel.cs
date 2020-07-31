namespace WebApi.Web.ViewModels.Articles
{
    using AutoMapper;
    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class ArticleCreateViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string Username { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                .CreateMap<Article, ArticleCreateViewModel>()
                .ForMember(
                    d => d.Username,
                    opt => opt.MapFrom(src => src.Author.UserName));
        }
    }
}
