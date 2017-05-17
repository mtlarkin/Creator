using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Creator.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Creator.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly CreatorDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController (UserManager<ApplicationUser> userManager, CreatorDbContext db)
        { 
            _userManager = userManager;
            _db = db;

        }


        /// <summary>
        /// GET: Post/Index with current user's Posts
        /// </summary>
        /// <returns>'Index' view with a list of the current user's Posts</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);


            return View(_db.Posts.Where(x => x.PostOwner.Id == currentUser.Id));
        }

        /// <summary>
        /// GET: Post/Create
        /// </summary>
        /// <returns> 'Create' view</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// POST: Post/Create - Creates a new post with information submitted from the 'Create' view
        /// </summary>
        /// <param name="post">Takes the information entered in the 'Create' view and inserts it into a temporary Post object that will be saved to the database</param>
        /// <returns>Redirect to 'Post/Index'</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
          //Grab the id of the current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          //Find the current User object in the database by searching for the user's id
            var currentUser = await _userManager.FindByIdAsync(userId);
          //Assign the current user as the post's owner
            post.PostOwner = currentUser;

          //Add the post to the database and save it
            _db.Posts.Add(post);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get the details and comments of the clicked post
        /// </summary>
        /// <param name="postId">Extracted from the Post object's link</param>
        /// <returns>View of a posts details</returns>
        [HttpGet]
        public IActionResult ViewPost(int postId)
        {
            //Grab the selected post from the database and include the comments of that post.
            var post = _db.Posts.Include(p => p.Comments).ThenInclude(r => r.CommentReplies).FirstOrDefault(p => p.PostId == postId);
            ViewBag.Post = post;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment, int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var post = _db.Posts.FirstOrDefault(p => p.PostId == id);
            comment.CommentOwner = currentUser;
            comment.PostRepliedTo = post;

            _db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddReply(Comment reply, int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var comment = _db.Comments.FirstOrDefault(c => c.CommentId == id);
            reply.CommentRepliedToId = comment;
            reply.CommentOwner = currentUser;

            return RedirectToAction("Index");
        }
    }
}
