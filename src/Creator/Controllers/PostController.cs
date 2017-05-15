using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Creator.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            return View(_db.Posts.Where(x => x.PostOwner.Id == currentUser.Id));
        }

        /// <summary>
        /// GET: Post/Create
        /// </summary>
        /// <returns> 'Create' view</returns>
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Creates a new post with information submitted from the 'Create' view
        /// </summary>
        /// <param name="post">Takes the information entered in the 'Create' view and inserts it into a temporary Post object that will be saved to the database</param>
        /// <returns>Redirect to 'Post/Index'</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            post.PostOwner = currentUser;
            _db.Posts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            comment.CommentOwner = currentUser;
            return RedirectToAction("Index");
        }
    }
}
