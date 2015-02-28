namespace ForumSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Vote { get; set; }

        public ICollection<TagViewModel> Tags { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Post, IndexBlogPostViewModel>()
                .ForMember(p => p.Vote, opt => opt.MapFrom(p => p.Vote.Value));
        }
    }
}