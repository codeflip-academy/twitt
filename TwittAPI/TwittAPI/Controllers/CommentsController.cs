using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwittAPI.Models;
using Microsoft.Extensions.Configuration;
using TwittAPI.Presentation;

namespace TwittAPI.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private IConfiguration _config;

        private readonly TwittContext _context;

        public CommentsController(TwittContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("api/posts/{id}/comments")]
        public IActionResult GetPostComments(int id, [FromQuery] int page)
        {
            var pageSize = Convert.ToInt32(_config.GetSection("Pagination")["CommentPageSize"]);

            if (page < 1)
            {
                return BadRequest("Requsted page must be greater than 0.");
            }

            var commentCount = _context.CommentsCount.Where(c => c.MessageId == id).FirstOrDefault();
            int numberOfComments = commentCount.CommentCount != null ? (int)commentCount.CommentCount : 0;
            var pages = numberOfComments / pageSize;

            if (numberOfComments % pageSize != 0)
            {
                pages++;
            }

            if (page > pages)
            {
                return NotFound("The requested page does not exist.");
            }

            var rowsToSkip = (page - 1) * pageSize;
            var comments = _context.Comment
                .Where(
                    x => _context.Comment
                    .OrderBy(y => y.Id)
                    .Select(y => y.Id)
                    .Skip(rowsToSkip)
                    .Take(pageSize)
                    .Contains(x.Id) && x.MessageId == id
                )
                .Include("Profile")
                .ToList();

            var commentList = new List<CommentPresentation>();

            var profiles = new Dictionary<string, PostProfilePresentation>();
            var commentProfiles = comments.Select(c => new { c.Profile.FullName, c.Profile.UserName, c.Profile.Picture }).Distinct();

            // Store all distinct usernames in dictionary
            foreach (var c in commentProfiles)
            {
                profiles.Add(c.UserName, new PostProfilePresentation(c.FullName, c.UserName, c.Picture));
            }

            foreach (var comment in comments)
            {
                var commentPresentation = new CommentPresentation(comment.Id, comment.Message, comment.Profile);
                commentList.Add(commentPresentation);
            }

            var commentFeed = new CommentFeed(commentList, page, pages);

            return Ok(new { commentFeed, profiles });
        }

        // POST: api/comments
        [HttpPost("api/profile/{profileid}/comment")]
        public IActionResult PostComment(int profileId, [FromBody] Comment comment)
        {
            comment.ProfileId = profileId;

            var profile = _context.Profile.Find(profileId);
            var messageHelper = new MessageHelper();
            var helper = messageHelper.IsProfileActive(profile);

            if (helper == false)
            {
                return BadRequest("User is not authorized to post");
            }

            if (comment.ProfileId != profileId)
            {
                return BadRequest();
            }

            if (comment.Message.Length > 200)
            {
                return BadRequest("Comment must be 200 characters or less");
            }

            _context.Comment.Add(comment);

            _context.SaveChanges();

            return Ok(comment);
        }

        // DELETE: api/comments/id
        [HttpDelete("api/comment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            var twittCS = new TwittContextService(_config, _context);
            if (twittCS.DeleteComment(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
