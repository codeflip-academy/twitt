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
                    stringValue = "Like";
                    break;
                case false:
                    stringValue = "DisLike";
                    break;
                default:
                    stringValue = "";
                    break;

            }
            return stringValue;
        }
    }
}