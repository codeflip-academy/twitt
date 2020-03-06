using System;

namespace TwittAPI
{
    public static class ProfileState
    {
    
        public static bool Active = true;
        public static bool Deleted = false;
        
        public static string ConvertToString(bool state)
        {
            string stringValue;
            
            switch (state)
            {
                case true:
                    stringValue = "Active";
                    break;
                case false:
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





