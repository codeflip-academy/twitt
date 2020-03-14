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

    //[Route("api/[controller]")]
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

        [HttpGet("api/[controller]")]
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
                    .Contains(x.Id)
                )
                .Include("Profile")
                .Include("Reaction")
                .ToList();

            var postList = new List<PostPresentation>();
            var profiles = new Dictionary<string, PostProfilePresentation>();
            var postProfiles = posts.Select(p => new { p.Profile.FullName, p.Profile.UserName, p.Profile.Picture } ).Distinct();

            // Store all distinct usernames in dictionary
            foreach(var p in postProfiles)
            {
                profiles.Add(p.UserName, new PostProfilePresentation(p.FullName, p.UserName, p.Picture));
            }

            // Add each post to post presentation
            foreach (var post in posts)
            {
                var commentCountOrNull = _context.CommentsCount.Where(c => c.MessageId == post.Id).FirstOrDefault();
                var commentCount = commentCountOrNull == null ? 0 : commentCountOrNull.CommentCount;

                postList.Add(new PostPresentation(post, commentCount));
            }

            var newsFeed = new PostFeed(postList, page, pages);

            return Ok(new { newsFeed, profiles });
        }

        // POST: api/posts
        [HttpPost("api/profile/{profileid}/post")]
        public IActionResult PostMessage(int profileId, [FromBody] MessageModels post)
        {
            var profile = _context.Profile.Find(profileId);
            var messageHelper = new MessageHelper();
            var helper = messageHelper.IsProfileActive(profile);
            
            if (helper == false)
            {
                return BadRequest("User is not authorized to post");
            }

            var p = new Message();

            post.ProfileId = profileId;

            if (post.ProfileId != profileId)
            {
                return BadRequest();
            }

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

        [HttpDelete("api/post/{id}")]
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
