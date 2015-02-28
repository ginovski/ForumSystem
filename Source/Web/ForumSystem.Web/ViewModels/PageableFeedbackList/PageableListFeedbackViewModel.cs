namespace ForumSystem.Web.ViewModels.PageableFeedbackList
{
    using System;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PageableListFeedbackViewModel : IMapFrom<Feedback>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Feedback, PageableListFeedbackViewModel>()
                .ForMember(f => f.AuthorName, opt => opt.MapFrom(f => f.Author.UserName));
        }
    }
}