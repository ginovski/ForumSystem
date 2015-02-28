namespace ForumSystem.Web.Areas.Administration.ViewModels.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class DisplayPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        [UIHint("MultiLineText")]
        public string Content { get; set; }

        [UIHint("SingleLineText")]
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Post, DisplayPostViewModel>()
                .ForMember(p => p.AuthorName, opt => opt.MapFrom(p => p.Author.UserName));
        }
    }
}