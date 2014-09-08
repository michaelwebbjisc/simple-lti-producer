using LtiLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace LtiLibrary.OAuth
{
    public static class OAuthUtility
    {
        /// <summary>
        /// Generate the signature base that is used to produce the signature
        /// </summary>
        /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
        /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
        /// <param name="parameters"></param>
        /// <returns>The signature base</returns>
        internal static string GenerateSignatureBase(string httpMethod, Uri url, NameValueCollection parameters)
        {
            var normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                normalizedUrl += ":" + url.Port;
            }
            normalizedUrl += url.AbsolutePath;

            StringBuilder signatureBase = new StringBuilder();
            signatureBase.Append(httpMethod.ToRfc3986EncodedString().ToUpper()).Append('&');
            signatureBase.Append(normalizedUrl.ToRfc3986EncodedString()).Append('&');

            var excludedNames = new List<string> {OAuthConstants.SignatureParameter};
            signatureBase.Append(parameters.ToNormalizedString(excludedNames).ToRfc3986EncodedString());

            return signatureBase.ToString();
        }

        /// <summary>
        /// Generates a signature using the specified signatureType 
        /// </summary>
        /// <param name="httpMethod">The http method used</param>
        /// <param name="url">The full url to be signed</param>
        /// <param name="parameters">The collection of parameters to sign</param>
        /// <param name="consumerSecret">The OAuth consumer secret used to generate the signature</param>
        /// <returns>A base64 string of the hash value</returns>
        public static string GenerateSignature(string httpMethod, Uri url, NameValueCollection parameters, string consumerSecret)
        {
            var signatureBase = GenerateSignatureBase(httpMethod, url, parameters);

            // Note that in LTI, the TokenSecret (second part of the key) is blank
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}&", consumerSecret.ToRfc3986EncodedString()));

            var dataBuffer = Encoding.ASCII.GetBytes(signatureBase);
            var hashBytes = hmacsha1.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
