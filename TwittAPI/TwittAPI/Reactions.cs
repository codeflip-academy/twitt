using System;

namespace TwittAPI
{
    public static class Reactions
    {
        public static bool Like = true;
        public static bool DisLike = false;

        public static string ConvertToString(bool state)
        {
            var stringValue = "";

            switch(state)
            {
                case true:
                    stringValue = "Liked";
                    break;
                case false:
                    stringValue = "DisLiked";
                    break;
                default:
                    stringValue = "";
                    break;

            }
            return stringValue;
        }
    }
}