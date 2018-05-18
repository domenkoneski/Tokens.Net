using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Tokens.Utils
{
    public class Networking
    {
        /// <summary>
        /// Retrieves endpoint content with signing the API credentials.
        /// </summary>
        /// <param name="endpoint">URL</param>
        /// <param name="credentials">Credentials with API and secret key</param>
        /// <returns></returns>
        public static string GetResponseSigned(string endpoint, Credentials credentials, params KeyValuePair<String, String>[] keyValues)
        {
            WebRequest request = WebRequest.Create(endpoint + AssemblePayload(keyValues));
            request.Method = "GET";

            string signature = credentials.GetSignature();

            request.Headers.Add("key", credentials.ApiKey);
            request.Headers.Add("nonce", credentials.LastUsedNonce + "");
            request.Headers.Add("signature", signature);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), true))
                return reader.ReadToEnd();
            
        }

        /// <summary>
        /// Retrieve endpoint content without signature, for public methods.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static string GetResponse(string endpoint, params KeyValuePair<String, String>[] keyValues)
        {
            WebRequest request = WebRequest.Create(endpoint + AssemblePayload(keyValues));
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), true))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Post request with signature and payload if any.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static string PostResponseSigned(string endpoint, Credentials credentials, KeyValuePair<String, String>[] keyValues)
        {
            using (var webClient = new WebClient())
            {
                string signature = credentials.GetSignature();

                webClient.Headers.Add("key", credentials.ApiKey);
                webClient.Headers.Add("nonce", credentials.LastUsedNonce + "");
                webClient.Headers.Add("signature", signature);

                byte[] response = webClient.UploadValues(endpoint, "POST", ToNameValueCollection(keyValues));

                return Encoding.UTF8.GetString(response);
            }
        }

        /// <summary>
        /// Assembles payload from keyvalues using string builder instead of concating.
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static string AssemblePayload(params KeyValuePair<String, String>[] keyValues)
        {
            if (keyValues == null || keyValues.Length == 0)
                return "";

            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < keyValues.Length; i++)
            {
                buffer.Append(keyValues[i].Key);
                buffer.Append("=");
                buffer.Append(keyValues[i].Value);

                if (i != keyValues.Length - 1)
                    buffer.Append("&");
            }

            return buffer.ToString();
        }

        static NameValueCollection ToNameValueCollection(KeyValuePair<String, String>[] keyValues)
        {
            var data = new NameValueCollection();

            if (keyValues == null || keyValues.Length == 0)
                return data;

            foreach (KeyValuePair<String, String> pair in keyValues)
                data[pair.Key] = pair.Value;
       
            return data;
        }
    }
}
