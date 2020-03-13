using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwittAPI.Models
{
    public class TwittContextService
    {
        public TwittContextService(IConfiguration config, TwittContext context)
        {
            _config = config;
            _context = context;
        }

        private IConfiguration _config;
        private readonly TwittContext _context;

        public bool DeleteReaction(int id)
        {
            //Logic to delete a comment by id
            var reaction = _context.Reaction.Where(c => c.Id == id).FirstOrDefault();

            if (reaction == null)
            {
                return false;
            }
            _context.Remove(reaction);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteComment(int id)
        {
            //Logic to delete a comment by id
            var comment = _context.Comment.Where(c => c.Id == id).FirstOrDefault();

            if (comment == null)
            {
                return false;
            }
            _context.Remove(comment);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteMessage(int id)
        {
            //DeleteReactions();

            //Get all the reactions on a message / post ID
            var reactions = _context.Reaction.Where(c => c.Message == id);

            //Loop through the reactions connected to the messageID and delete them
            if (reactions != null)
            {
                foreach (var reaction in reactions)
                {
                    _context.Remove(reaction);
                }
            }

            //Get all the comments on message/post ID
            var comments = _context.Comment.Where(c => c.MessageId == id);

            //Loop through the comments connected to the messageID and delete them
            foreach (var comment in comments)
            {
                _context.Remove(comment);
            }

            //Get the messageID and delete the message 
            var message = _context.Message.Where(c => c.Id == id).FirstOrDefault();

            if (message == null)
            {
                return false;
            }

            _context.Remove(message);
            _context.SaveChanges();

            return true;
        }
    }
}
