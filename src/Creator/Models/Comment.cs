using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Creator.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Body { get; set; }
        /// <summary>
        /// The user that created this comment
        /// </summary>
        public virtual ApplicationUser CommentOwner { get; set; }
        /// <summary>
        /// The post this comment is replying to
        /// </summary>
        public virtual Post PostRepliedTo { get; set; }
        /// <summary>
        /// The ID of the comment that *this* comment is replying to
        /// </summary>
        public virtual Comment CommentRepliedToId { get; set; }
        /// <summary>
        /// Comments in reply to this comment
        /// </summary>
        public virtual ICollection<Comment> CommentReplies { get; set; }

    }
}
