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
    }
}
