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
        public int Score { get; set; }
        public int Bump { get; set; }
        public int Knock { get; set; }
        public virtual Post PostCommentedOn { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ApplicationUser CommentOwner { get; set; }

    }
}
