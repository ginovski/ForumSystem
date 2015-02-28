namespace ForumSystem.Web.ViewModels.Home
{
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}