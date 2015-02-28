namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure;
    using ForumSystem.Web.ViewModels.Feedbacks;

    using Microsoft.AspNet.Identity;

    public class FeedbackController : Controller
    {
        private readonly ISanitizer sanitizer;
        private IDeletableEntityRepository<Feedback> feedbacks;

        public FeedbackController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer)
        {
            this.feedbacks = feedbacks;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedbackViewModel feedback)
        {
            if (feedback != null && ModelState.IsValid)
            {
                var newFeedback = Mapper.Map<Feedback>(feedback);
                newFeedback.AuthorId = User.Identity.GetUserId();
                newFeedback.Content = this.sanitizer.Sanitize(feedback.Content);

                this.feedbacks.Add(newFeedback);
                this.feedbacks.SaveChanges();

                return this.Redirect("/Home/Index");
            }

            return this.View(feedback);
        }
    }
}