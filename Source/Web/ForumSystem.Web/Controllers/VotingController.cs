namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class VotingController : Controller
    {
        private IRepository<ApplicationUser> users;
        private IRepository<Post> posts;

        public VotingController(IRepository<Post> posts, IRepository<ApplicationUser> users)
        {
            this.posts = posts;
            this.users = users;
        }

        [HttpPost]
        public ActionResult Vote(int id, int vote)
        {
            var postVote = this.posts.GetById(id).Vote;
            var currentUser = this.users.GetById(User.Identity.GetUserId());
            if (!postVote.Users.Contains(currentUser))
            {
                postVote.Value += vote;

                this.posts.SaveChanges();
            }

            return this.Content(postVote.Value.ToString());
        }
    }
}