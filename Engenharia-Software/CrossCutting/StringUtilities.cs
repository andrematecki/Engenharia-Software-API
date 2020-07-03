using System.Net;

namespace Engenharia_Software.CrossCutting
{
    public class StringUtilities
    {
        public static string UrlEncode(string value)
        {
            // Temporarily replace spaces with the literal -SPACE-
            string url = value.Replace(" ", "-SPACE-");
            url = WebUtility.UrlEncode(url);

            // Some servers have issues with ( and ), but UrlEncode doesn't 
            // affect them, so we include those in the encoding as well.
            return url.Replace("-SPACE-", "%20");
        }
    }
}
