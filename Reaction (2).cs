using System;

namespace twit
{
    public static class Reaction
    {
        public static byte Like = 0;
        public static byte DisLike = 1;

        public static string ConvertToString(byte state)
        {
            var stringValue = "";

            switch(state)
            {
                case 0:
                    stringValue = "Liked";
                    break;
                case 1:
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