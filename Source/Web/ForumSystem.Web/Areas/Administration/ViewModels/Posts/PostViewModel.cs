namespace ForumSystem.Web.Areas.Administration.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}