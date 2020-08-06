namespace ForumSystem.Web.ViewModels.Votes
{
    public class VoteResponseModel
    {
        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        public int VotesScore => this.UpVotes - this.DownVotes;
    }
}
