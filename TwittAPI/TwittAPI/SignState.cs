using System;

namespace twitt
{
    public static class SignInState
    {
        public static byte SignedIn = 0;
        public static byte SignedOut = 1;

        public static string ConvertToString(byte state)
        {
            string stringValue;

            switch(state)
            {
                case 0:
                    stringValue = "SignedIn";
                    break;
                case 1:
                    stringValue = "SignedOut";
                    break;
                default:
                    stringValue = "";
                    break;
            }
            return stringValue;
        }
    }

}