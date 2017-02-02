using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect_API.Accounts
{   
    public class AccessToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        [JsonProperty("accessToken")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        [JsonProperty("expiresIn")]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="connectUrl">The connect URL.</param>
        /// <param name="code">The code.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="clientID">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns></returns>
        public static AccessToken GetAccessToken(string connectUrl, string code, string redirectUri, string clientID, string clientSecret)
        {
            var client = new RestClient(connectUrl);
            var request = new RestRequest(@"apps/token?grant_type=authorization_code&code=" + code + "&redirect_uri=" + redirectUri +
                "&client_id=" + clientID + "&client_secret=" + clientSecret);
            var tokenResponse = client.Execute<AccessToken>(request);
            return JsonConvert.DeserializeObject<AccessToken>(tokenResponse.Content);
        }

        public static AccessToken RefreshAccessToken(string connectUrl, string token, string clientID, string clientSecret)
        {
            var client = new RestClient(connectUrl);
            var request = new RestRequest(@"apps/token?grant_type=refresh_token&refresh_token=" + token +
                "&client_id=" + clientID + "&client_secret=" + clientSecret);
            var tokenResponse = client.Execute<AccessToken>(request);
            return JsonConvert.DeserializeObject<AccessToken>(tokenResponse.Content);
        }
    }
}