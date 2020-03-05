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
            var pageSize = Convert.ToInt32(_config.GetSection("Pagination")["PageSize"]);

            if(page < 1)
            {
                return BadRequest("Requsted page must be greater than 0.");
            }

            var numberOfPosts = _context.Post.Count();
            var pages = numberOfPosts / pageSize;

            if (numberOfPosts % pageSize != 0)
            {
                pages++;
            }

            if(page > pages)
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
                .ToList();

            return Ok(new PostFeed(posts, page, pages));
        }
        
    }
}
