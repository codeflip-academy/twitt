using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwittAPI.Models;
using TwittAPI.Presentation;

namespace TwittAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private IConfiguration _config;

        private readonly TwittContext _context;

        public PostsController(TwittContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Posts
        [HttpGet]
        public IActionResult GetPosts([FromQuery] int page)
        {
            var pageSize = Convert.ToInt32(_config.GetSection("Pagination")["PostPageSize"]);

            if (page < 1)
            {
                return BadRequest("Requsted page must be greater than 0.");
            }

            var numberOfPosts = _context.Post.Count();
            var pages = numberOfPosts / pageSize;

            if (numberOfPosts % pageSize != 0)
            {
                pages++;
            }

            if (page > pages)
            {
                return NotFound("The requested page does not exist.");
            }

            var rowsToSkip = (page - 1) * pageSize;
            var posts = _context.Post
                .Where(
                    x => _context.Post
                    .OrderBy(y => y.Id)
                    .Select(y => y.Id)
                    .Skip(rowsToSkip)
                    .Take(pageSize)
                    .Contains(x.Id)
                )
                .Include("Profile")
                .ToList();

            var postList = new List<PostPresentation>();

            foreach(var post in posts)
            {
                var likes = _context.Reaction.Where(l => l.State == true && l.Post == post.Id).Count();
                var dislikes = _context.Reaction.Where(l => l.State == false && l.Post == post.Id).Count();
                var commentCount = _context.CommentsCount.Where(c => c.PostId == post.Id).FirstOrDefault();
                if(commentCount == null)
                {
                    postList.Add(new PostPresentation(post, 0, likes, dislikes));
                }
                else
                {
                    postList.Add( new PostPresentation(post, commentCount.CommentCount, likes, dislikes) );
                }
            }

            return Ok(new PostFeed(postList, page, pages));
        }

        // POST: api/posts
        [HttpPost]
        public IActionResult PostMessage([FromBody] Post post)
        {
            if (post.Message.Length > 200)
            {
                return BadRequest("Message must be 200 characters or less");
            }

            _context.Post.Add(post);

            //Check if an image exists, if one does use the ImageConverter

            _context.SaveChanges();

            return Ok(post);
        }
    }
}
