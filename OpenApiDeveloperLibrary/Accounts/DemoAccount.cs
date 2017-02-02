using Newtonsoft.Json;
using RestSharp;

namespace Connect_API.Accounts
{
    /// <summary>
    /// The demo account class is used for creating a new demo account using the Connect API RESTful web services.
    /// </summary>
    public class DemoAccount
    {
        /// <summary>
        /// The country identifier
        /// </summary>
        [JsonProperty("countryId")]
        public long CountryID { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The leverage
        /// </summary>
        [JsonProperty("leverage")]
        public int Leverage { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        /// <summary>
        /// The deposit currency
        /// </summary>
        [JsonProperty("depositCurrency")]
        public string DepositCurrency { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// The account type
        /// </summary>
        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        /// <summary>
        /// Creates the demo account.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="testAccessToken">The test access token.</param>
        /// <returns></returns>
        public string CreateDemoAccount(string apiUrl, string testAccessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/createdemo/?oauth_token=" + testAccessToken, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(this);
            return client.Execute(request).StatusDescription;
        }
    }
}