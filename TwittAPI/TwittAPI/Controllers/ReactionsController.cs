using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwittAPI.Models;
using Microsoft.Extensions.Configuration;


namespace TwittAPI.Controllers
{
    [ApiController]
    public class ReactionsController : ControllerBase
    {

        private IConfiguration _config;

        private readonly TwittContext _context;

        public ReactionsController(TwittContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("api/posts/{postId}/reactions")]
        public IActionResult Reaction(int postId, [FromBody] ReactionModels reaction)
        {
            reaction.MessageID = postId; 
            var prevReaction = _context.Reaction.Where(x => x.Profile == reaction.ProfileID && x.Message == reaction.MessageID).FirstOrDefault();
            var stringValue = prevReaction == null ? "" : Reactions.ConvertToString(prevReaction.LikeOrDislike);

            if (reaction.ProfileID != 0 && reaction.MessageID != 0)
            {
                var react = new Reaction();
                var userReactions = _context.Reaction
                    .Where(r => r.Profile == reaction.ProfileID && r.Message == reaction.MessageID)
                    .Count();

                react.Message = reaction.MessageID;
                react.Profile = reaction.ProfileID;

                if (reaction.LikeOrDislike == "Like")
                {
                    react.LikeOrDislike = true;
                }
                else if (reaction.LikeOrDislike == "DisLike")
                {
                    react.LikeOrDislike = false;
                }

                if (userReactions < 1)
                {
                    _context.Reaction.Add(react);
                }
                else if (reaction.LikeOrDislike == stringValue)
                {
                    _context.Remove(prevReaction);
                }
                else if (reaction.LikeOrDislike != stringValue)
                {
                    _context.Reaction.Remove(prevReaction);
                    _context.Add(react);
                }

                else
                {
                    return BadRequest("Cannont give same reaction twice.");
                }

                _context.SaveChanges();
                return Ok(reaction);

            }
            return BadRequest("Data is missing from the request.");
        }
    }
}