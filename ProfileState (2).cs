using System;

namespace twitt
{
    public static class SignInState
    {
        public static byte Active = 0;
        public static byte Deleted = 1;

        public static string ConvertToString(byte state)
        {
            string stringValue;

            switch (state)
            {
                case 0:
                    stringValue = "Active";
                    break;
                case 1:
                    stringValue = "Deleted";
                    break;
                default:
                    stringValue = "";
                    break;
            }
            return stringValue;
        }
    }

}