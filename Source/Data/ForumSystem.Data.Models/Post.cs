namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public int VoteId { get; set; }

        public virtual Vote Vote { get; set; }

        public virtual ApplicationUser Author { get; set; }
 
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
