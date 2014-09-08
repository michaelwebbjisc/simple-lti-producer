using System;

namespace LtiLibrary.OAuth
{
    public static class OAuthConstants
    {
        public static string AuthScheme = "OAuth";
        public static string BodyHashParameter = "oauth_body_hash";
        public static string CallbackParameter = "oauth_callback";
        public static string CallbackDefault = "about:blank";
        public static string ConsumerKeyParameter = "oauth_consumer_key";
        public static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public static string NonceParameter = "oauth_nonce";
        public static string SignatureParameter = "oauth_signature"; 
        public static string SignatureMethodParameter = "oauth_signature_method";
        public static string SignatureMethodHmacSha1 = "HMAC-SHA1";
        public static string TimestampParameter = "oauth_timestamp";
        public static string VersionParameter = "oauth_version";
        public static string Version10 = "1.0";
    }
}
