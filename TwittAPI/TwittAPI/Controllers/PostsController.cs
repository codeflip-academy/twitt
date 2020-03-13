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

            var numberOfPosts = _context.Message.Count();
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
            var posts = _context.Message
                .Where(
                    x => _context.Message
                    .OrderBy(y => y.Id)
                    .Select(y => y.Id)
                    .Skip(rowsToSkip)
                    .Take(pageSize)
                    .Contains(x.Id) && x.Profile.Status != ProfileState.Deleted
                )
                .Include("Profile")
                .ToList();

            var postList = new List<PostPresentation>();

            foreach (var post in posts)
            {
                var like = Reactions.Like;
                var disLike = Reactions.DisLike;
                var likes = _context.Reaction.Where(l => l.LikeOrDislike == Reactions.Like && l.Message == post.Id).Count();
                var dislikes = _context.Reaction.Where(l => l.LikeOrDislike == Reactions.DisLike && l.Message == post.Id).Count();
                var commentCount = _context.CommentsCount.Where(c => c.MessageId == post.Id).FirstOrDefault();
                if (commentCount == null)
                {
                    postList.Add(new PostPresentation(post, 0, likes, dislikes));
                }
                else
                {
                    postList.Add(new PostPresentation(post, commentCount.CommentCount, likes, dislikes));
                }
            }



            return Ok(new PostFeed(postList, page, pages));
        }

        // POST: api/posts
        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageModels post)
        {
            var p = new Message();

            if (post.Message.Length > 200)
            {
                return BadRequest("Message must be 200 characters or less");
            }

            p.Text = post.Message;
            p.ProfileId = post.ProfileId;

            _context.Message.Add(p);

            _context.SaveChanges();

            post.Id = p.Id;

            if (post.Picture != null)
            {
                var image = new ImageHandler(connectionString: _config.GetConnectionString("TwittDatabase"));

                image.StoreImagePost(post);
            }

            return Ok(post);
        }

        // DELETE: api/posts/id
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var twittCS = new TwittContextService(_config, _context);

            if (twittCS.DeleteMessage(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
