using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwittAPI.Models;

namespace TwittAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly TwittContext _context;
        private IConfiguration _conifg;

        public PostsController(TwittContext context, IConfiguration config)
        {
            _context = context;
            _conifg = config;
        }

        // GET: api/Posts
        [HttpGet]
        public IActionResult GetPosts([FromQuery] int page)
        {
            if(page < 1)
            {
                return BadRequest("Requsted page must be greater than 0.");
            }

            var numberOfPosts = _context.Post.Count();
            var pages = numberOfPosts / 10;

            if (numberOfPosts % 10 != 0)
            {
                pages++;
            }

            if(page > pages)
            {
                return NotFound("The requested page does not exist.");
            }

            var rowsToSkip = (page - 1) * 10;

            var posts = _context.Post
                .Where(
                    x => _context.Post
                    .OrderBy(y => y.Id)
                    .Select(y => y.Id)
                    .Skip(rowsToSkip)
                    .Take(10)
                    .Contains(x.Id)
                )
                .ToList();

            return Ok(new PostFeed(posts, page, pages));
        }
        
    }
}
