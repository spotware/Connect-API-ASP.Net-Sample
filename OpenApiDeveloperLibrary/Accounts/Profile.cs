using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_API.Accounts
{
    /// <summary>
    ///  The Profice class is used for retrieving profile details for a specified token using the Connect API RESTful web services.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        [JsonProperty("userId")]
        public string UserID { get; set; }
        /// <summary>
        /// The nickname
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        /// <summary>
        /// The email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        public static Profile GetProfile(string apiUrl, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/profile/?oauth_token=" + accessToken);
            var responseProfile = client.Execute<Profile>(request);
            return JsonConvert.DeserializeObject<Profile>(responseProfile.Content);
        }
    }
}
