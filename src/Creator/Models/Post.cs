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
        /// Thought / Request / Offer 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Title of the post
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Body of the post
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// View counter that increments when someone clicks on the post
        /// </summary>
        public int Views { get; set; }
        /// <summary>
        /// Link to an external resource. It would be a good idea to include a 'WARNING' for external links
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Score, determined by bumps and knocks
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// +1 score for the post
        /// </summary>
        public int Bumps { get; set; }
        /// <summary>
        /// -1 score for the post
        /// </summary>
        public int Knocks { get; set; }
        /// <summary>
        /// If Type === Request, Available will be true of the work has not been accepted yet.
        /// </summary>
        public bool Available { get; set; }
        /// <summary>
        /// The User that made the post
        /// </summary>
        public virtual ApplicationUser PostOwner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Comment PostComment { get; set; }
    }
}
