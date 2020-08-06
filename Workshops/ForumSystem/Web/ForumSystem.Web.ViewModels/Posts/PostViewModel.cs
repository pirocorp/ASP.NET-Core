namespace ForumSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    using Ganss.XSS;

    public class PostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent
            => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpVotes => this.Votes.Count(v => v.Type == VoteType.UpVote);

        public int DownVotes => this.Votes.Count(v => v.Type == VoteType.DownVote);

        public int VotesScore => this.UpVotes - this.DownVotes;

        public ICollection<PostVoteViewModel> Votes { get; set; }

        public PostUserVoteViewModel UserVote { get; set; }

        public bool UserVoteIsUp => this.UserVote?.Type == VoteType.UpVote;

        public bool UserVoteIsDown => this.UserVote?.Type == VoteType.DownVote;
    }
}
