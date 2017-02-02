using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    /// The deal class is used for retrieving deals for a specified account using the Connect API RESTful web services.
    /// </summary>
    public class Deal
    {
        /// <summary>
        /// Gets or sets the deal identifier.
        /// </summary>
        /// <value>
        /// The deal identifier.
        /// </value>
        [JsonProperty("dealId")]
        public string DealID { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        [JsonProperty("positionId")]
        public string PositionID { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        [JsonProperty("orderId")]
        public string OrderID { get; set; }

        /// <summary>
        /// Gets or sets the trade side.
        /// </summary>
        /// <value>
        /// The trade side.
        /// </value>
        [JsonProperty("tradeSide")]
        public string TradeSide { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        [JsonProperty("volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the filled volume.
        /// </summary>
        /// <value>
        /// The filled volume.
        /// </value>
        [JsonProperty("filledVolume")]
        public string FilledVolume { get; set; }

        /// <summary>
        /// Gets or sets the name of the symbol.
        /// </summary>
        /// <value>
        /// The name of the symbol.
        /// </value>
        [JsonProperty("symbolName")]
        public string SymbolName { get; set; }

        /// <summary>
        /// Gets or sets the commision.
        /// </summary>
        /// <value>
        /// The commision.
        /// </value>
        [JsonProperty("commision")]
        public string Commision { get; set; }

        /// <summary>
        /// Gets or sets the execution price.
        /// </summary>
        /// <value>
        /// The execution price.
        /// </value>
        [JsonProperty("executionPrice")]
        public string ExecutionPrice { get; set; }

        /// <summary>
        /// Gets or sets the base to usd conversion rate.
        /// </summary>
        /// <value>
        /// The base to usd conversion rate.
        /// </value>
        [JsonProperty("baseToUsdConversionRate")]
        public string BaseToUSDConversionRate { get; set; }

        /// <summary>
        /// Gets or sets the margin rate.
        /// </summary>
        /// <value>
        /// The margin rate.
        /// </value>
        [JsonProperty("marginRate")]
        public string MarginRate { get; set; }

        /// <summary>
        /// Gets or sets the test partner.
        /// </summary>
        /// <value>
        /// The test partner.
        /// </value>
        [JsonProperty("TestPartner")]
        public string TestPartner { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the create timestamp.
        /// </summary>
        /// <value>
        /// The create timestamp.
        /// </value>
        [JsonProperty("createTimestamp")]
        public string CreateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the execution timestamp.
        /// </summary>
        /// <value>
        /// The execution timestamp.
        /// </value>
        [JsonProperty("executionTimestamp")]
        public string ExecutionTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the position close details.
        /// </summary>
        /// <value>
        /// The position close details.
        /// </value>
        [JsonProperty("positionCloseDetails")]
        public PositionCloseDetails PositionCloseDetails { get; set; }

        /// <summary>
        /// Gets the deals.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static List<Deal> GetDeals(string apiUrl, string accountID, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/deals?oauth_token=" + accessToken);
            var responseDeal = client.Execute<Deal>(request);
            return JsonConvert.DeserializeObject<List<Deal>>((JObject.Parse(responseDeal.Content)["data"]).ToString());
        }
    }
}