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
    ///  The TradingAccount class is used for retrieving trading accounts for a specified user using the Connect API RESTful web services.
    /// </summary>
    public class TradingAccount
    {
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        [JsonProperty("accountID")]
        public string AccountID { get; set; }
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }
        /// <summary>
        /// Gets or sets the live.
        /// </summary>
        /// <value>
        /// The live.
        /// </value>
        [JsonProperty("live")]
        public string Live { get; set; }
        /// <summary>
        /// Gets or sets the broker title.
        /// </summary>
        /// <value>
        /// The broker title.
        /// </value>
        [JsonProperty("brokerTitle")]
        public string BrokerTitle { get; set; }
        /// <summary>
        /// Gets or sets the broker name identifier.
        /// </summary>
        /// <value>
        /// The broker name identifier.
        /// </value>
        [JsonProperty("brokerNameId")]
        public string BrokerNameId { get; set; }
        /// <summary>
        /// Gets or sets the deposit currency.
        /// </summary>
        /// <value>
        /// The deposit currency.
        /// </value>
        [JsonProperty("depositCurrency")]
        public string DepositCurrency { get; set; }
        /// <summary>
        /// Gets or sets the trader registration timestamp.
        /// </summary>
        /// <value>
        /// The trader registration timestamp.
        /// </value>
        [JsonProperty("traderRegistrationTimestamp")]
        public string TraderRegistrationTimestamp { get; set; }
        /// <summary>
        /// Gets or sets the leverage.
        /// </summary>
        /// <value>
        /// The leverage.
        /// </value>
        [JsonProperty("leverage")]
        public string Leverage { get; set; }
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        [JsonProperty("balance")]
        public string Balance { get; set; }
        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>
        /// The deleted.
        /// </value>
        [JsonProperty("deleted")]
        public string Deleted { get; set; }

        /// <summary>
        /// Gets the trading accounts.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static List<TradingAccount> GetTradingAccounts(string apiUrl, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/?oauth_token=" + accessToken);
            var responseCashflow = client.Execute<TradingAccount>(request);
            return JsonConvert.DeserializeObject<List<TradingAccount>>((JObject.Parse(responseCashflow.Content)["data"]).ToString());
        }
    }
}
