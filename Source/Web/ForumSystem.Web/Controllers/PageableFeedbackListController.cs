namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.ViewModels.PageableFeedbackList;

    [Authorize]
    public class PageableFeedbackListController : Controller
    {
        private const int InitialPageSize = 4;

        private IDeletableEntityRepository<Feedback> feedbacks;

        public PageableFeedbackListController(IDeletableEntityRepository<Feedback> feedbacks)
        {
            this.feedbacks = feedbacks;
        }

        public ActionResult Index(int? page)
        {
            int currentPage;
            if (page == null || page < 1)
            {
                currentPage = 0;
            }
            else
            {
                currentPage = (int)page - 1;
            }

            ViewBag.NumberOfPages = this.feedbacks.All().Count() / 4;
            ViewBag.CurrentPage = currentPage + 1;

            var viewModel = this.feedbacks
                .All()
                .OrderBy(f => f.Id)
                .Skip(currentPage * InitialPageSize)
                .Take(InitialPageSize)
                .Project()
                .To<PageableListFeedbackViewModel>();

            return this.View(viewModel);
        }
    }
}