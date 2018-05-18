using System;
using System.Security.Cryptography;
using System.Text;

namespace Tokens
{
    public class Credentials
    {
        public string ApiKey { get; }
        public string SecretKey { get; }
        public long LastUsedNonce { get; set; }

        private DateTime epoch = new DateTime(1970, 1, 1);

        public Credentials(string apiKey, string secretKey)
        {
            this.ApiKey = apiKey;
            this.SecretKey = secretKey;
            this.LastUsedNonce = int.MinValue;
        }

        /// <summary>
        /// Sign nonce + api key with secret key.
        /// </summary>
        /// <returns></returns>
        public string GetSignature()
        {
            long nonce = GetNonce();

            if (nonce == LastUsedNonce)
                nonce++;

            LastUsedNonce = nonce;

            byte[] signature = HashHMAC(
                    Encoding.UTF8.GetBytes(this.SecretKey),
                    Encoding.UTF8.GetBytes(string.Format("{0}{1}", nonce, this.ApiKey))
            );

            StringBuilder hexDigest = new StringBuilder();
            foreach (byte b in signature)
                hexDigest.Append(String.Format("{0:x2}", b).ToUpper());
            

            return hexDigest.ToString();
        }

        /// <summary>
        /// Get nonce, total milliseconds from epoch will do the work.
        /// </summary>
        /// <returns></returns>
        long GetNonce()
        {
            return (long)(DateTime.Now - this.epoch).TotalMilliseconds;
        }

        /// <summary>
        /// Hmac hash.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }
}
