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
        public string Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Views { get; set; }
        public string Link { get; set; }
        public int Bumps { get; set; }
        public int Knocks { get; set; }
        public virtual ApplicationUser PostOwner { get; set; }
        public virtual Comment PostComment { get; set; }
    }
}
