namespace ForumSystem.Web.ViewModels.Feedbacks
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class FeedbackViewModel : IMapFrom<Feedback>
    {
        [StringLength(20)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [AllowHtml]
        [UIHint("MultiLineText")]
        public string Content { get; set; }
    }
}