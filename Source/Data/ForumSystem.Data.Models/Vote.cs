namespace ForumSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Data.Common.Models;

    public class Vote : AuditInfo
    {
        public Vote()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
