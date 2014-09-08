using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;

namespace LtiLibrary.Extensions
{
    public static class AuthorizationHeaderValueExtensions
    {
        /// <summary>
        /// Parse the AuthenticationHeaderValue.Parameter into a NameValueCollection.
        /// </summary>
        /// <param name="authorizationHeader">The AuthorizationHeaderValue to parse.</param>
        /// <returns>A NameValueCollection of all the parameters found.</returns>
        public static NameValueCollection ParseParameter(this AuthenticationHeaderValue authorizationHeader)
        {
            var parameters = new NameValueCollection();
            foreach (var pair in authorizationHeader.Parameter.Split(','))
            {
                var keyValue = pair.Split('=');
                var key = keyValue[0].Trim();
                var value = HttpUtility.UrlDecode(keyValue[1].Trim('"'));

                // Ignore invalid key/value pairs
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    parameters.AddParameter(key, value);
                }
            }
            return parameters;
        }
    }
}
