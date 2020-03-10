﻿using System;
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

            var commentCount = _context.CommentsCount.Where(c => c.PostId == id).FirstOrDefault();
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
                    .Contains(x.Id) && x.PostId == id
                )
                .Include("Profile")
                .ToList();

            var commentList = new List<CommentPresentation>();

            foreach(var comment in comments)
            {
                var commentPresentation = new CommentPresentation(comment.Id, comment.Message, comment.Profile);
                commentList.Add(commentPresentation);
            }

            return Ok(new CommentFeed(commentList, page, pages));
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

        // DELETE: api/comments
        [HttpDelete("{id}")]
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
