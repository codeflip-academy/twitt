using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI
{
    public class MessageHelper
    {
        public bool IsProfileActive(Profile profile)
        {
            if (profile == null || profile.Status == ProfileState.Deleted)
            {
                return false;
            }
            return true;
        }
    }
}





