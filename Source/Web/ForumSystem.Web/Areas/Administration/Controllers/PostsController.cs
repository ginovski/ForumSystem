namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Areas.Administration.ViewModels.Posts;
    using ForumSystem.Web.Infrastructure;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Microsoft.AspNet.Identity;
	
	[Authorize]
    public class PostsController : Controller
    {
        private readonly ISanitizer sanitizer;
        private IDeletableEntityRepository<Post> posts;

        public PostsController(IDeletableEntityRepository<Post> posts, ISanitizer sanitizer)
        {
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var posts = this.posts
                .All()
                .Project()
                .To<DisplayPostViewModel>()
                .ToDataSourceResult(request);

            return this.Json(posts);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, DisplayPostViewModel model)
        {
            this.posts.Delete(model.Id);
            this.posts.SaveChanges();

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PostViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var post = this.posts.GetById(model.Id);
                post.Title = model.Title;
                post.Content = this.sanitizer.Sanitize(model.Content);

                this.posts.Update(post);
                this.posts.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, PostViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var post = new Post();
                post.Title = model.Title;
                post.Content = this.sanitizer.Sanitize(model.Content);
                post.AuthorId = User.Identity.GetUserId();
                post.Vote = new Vote
                {
                    Value = 0
                };

                this.posts.Add(post);
                this.posts.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}