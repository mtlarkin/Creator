using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Creator.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        /// <summary>
        /// The title of the post
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The body of the post
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// The user who created this post
        /// </summary>
        public virtual ApplicationUser PostOwner { get; set; }
        /// <summary>
        /// Comments replying to this post
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
