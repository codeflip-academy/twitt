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
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public IActionResult GetPostComments(int id, [FromQuery] int page)
        {
            var pageSize = Convert.ToInt32(_config.GetSection("Pagination")["CommentPageSize"]);

            if (page < 1)
            {
                return BadRequest("Requsted page must be greater than 0.");
            }

            var numberOfComments = _context.Comment.Count();
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
                    .Contains(x.Id) && x.PostId == id
                )
                .Include("Profile")
                .ToList();

            return Ok(new CommentFeed(comments, page, pages));
        }

        // POST: api/comments
        [HttpPost]
        public IActionResult PostComment([FromBody] Comment comment)
        {
            if (comment.Message.Length > 200)
            {
                return BadRequest("Comment must be 200 characters or less");
            }

            _context.Comment.Add(comment);

            _context.SaveChanges();

            return Ok(comment);
        }
    }
}
