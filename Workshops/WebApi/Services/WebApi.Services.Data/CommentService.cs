namespace WebApi.Services.Data
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Mapping;
    using WebApi.Data.Common.Repositories;
    using WebApi.Data.Models;
    using WebApi.Services.Models;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IArticleService articleService;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IArticleService articleService)
        {
            this.commentRepository = commentRepository;
            this.articleService = articleService;
        }

        public async Task<TResult> CreateAsync<TResult>(CreateCommentServiceModel model)
            where TResult : IMapFrom<Comment>
        {
            var article = await this
                .articleService
                .GetArticleByIdAsync(model.ArticleId);

            var comment = new Comment()
            {
                Article = article,
                Content = model.Content,
                Author = model.Author,
            };

            this.commentRepository.Add(comment);
            await this.commentRepository.SaveChangesAsync();

            return Mapper.Map<TResult>(comment);
        }
    }
}