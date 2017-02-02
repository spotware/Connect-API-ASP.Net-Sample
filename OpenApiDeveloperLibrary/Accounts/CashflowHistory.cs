using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    /// The cashflow history class is used for retrieving cashflow history for a specified account using the Connect API RESTful web services.
    /// </summary>
    public class CashflowHistory
    {
        /// <summary>
        /// Gets or sets the cashflow identifier.
        /// </summary>
        /// <value>
        /// The cashflow identifier.
        /// </value>
        [JsonProperty("cashflowId")]
        public string CashflowID { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the delta.
        /// </summary>
        /// <value>
        /// The delta.
        /// </value>
        [JsonProperty("delta")]
        public string Delta { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        [JsonProperty("balance")]
        public string Balance { get; set; }

        /// <summary>
        /// Gets or sets the balance version.
        /// </summary>
        /// <value>
        /// The balance version.
        /// </value>
        [JsonProperty("balanceVersion")]
        public string BalanceVersion { get; set; }

        /// <summary>
        /// Gets or sets the change timestamp.
        /// </summary>
        /// <value>
        /// The change timestamp.
        /// </value>
        [JsonProperty("changeTimestamp")]
        public string ChangeTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the equity.
        /// </summary>
        /// <value>
        /// The equity.
        /// </value>
        [JsonProperty("equity")]
        public string Equity { get; set; }

        /// <summary>
        /// Gets the cashflow history.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static List<CashflowHistory> GetCashflowHistory(string apiUrl, string accountID, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/cashflowhistory?oauth_token=" + accessToken);
            var responseCashflow = client.Execute<CashflowHistory>(request);
            return JsonConvert.DeserializeObject<List<CashflowHistory>>((JObject.Parse(responseCashflow.Content)["data"]).ToString());
        }
    }
}