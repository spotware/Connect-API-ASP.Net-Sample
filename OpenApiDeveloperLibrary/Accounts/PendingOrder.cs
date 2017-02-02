using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    /// The pending order class is used for retrieving deals for a specified account using the Connect API RESTful web services.
    /// </summary>
    public class PendingOrder
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        [JsonProperty("orderId")]
        public string OrderID { get; set; }

        /// <summary>
        /// Gets or sets the name of the symbol.
        /// </summary>
        /// <value>
        /// The name of the symbol.
        /// </value>
        [JsonProperty("symbolName")]
        public string SymbolName { get; set; }

        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// The type of the order.
        /// </value>
        [JsonProperty("orderType")]
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets the trade side.
        /// </summary>
        /// <value>
        /// The trade side.
        /// </value>
        [JsonProperty("tradeSide")]
        public string TradeSide { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        [JsonProperty("volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the stop loss.
        /// </summary>
        /// <value>
        /// The stop loss.
        /// </value>
        [JsonProperty("stopLoss")]
        public string StopLoss { get; set; }

        /// <summary>
        /// Gets or sets the take profit.
        /// </summary>
        /// <value>
        /// The take profit.
        /// </value>
        [JsonProperty("takeProfit")]
        public string TakeProfit { get; set; }

        /// <summary>
        /// Gets or sets the create timestamp.
        /// </summary>
        /// <value>
        /// The create timestamp.
        /// </value>
        [JsonProperty("createTimestamp")]
        public string CreateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the expiration timestamp.
        /// </summary>
        /// <value>
        /// The expiration timestamp.
        /// </value>
        [JsonProperty("expirationTimestamp")]
        public string ExpirationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the current price.
        /// </summary>
        /// <value>
        /// The current price.
        /// </value>
        [JsonProperty("currentPrice")]
        public string CurrentPrice { get; set; }

        /// <summary>
        /// Gets or sets the distance in pips.
        /// </summary>
        /// <value>
        /// The distance in pips.
        /// </value>
        [JsonProperty("distanceInPips")]
        public string DistanceInPips { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets the pending orders.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static List<PendingOrder> GetPendingOrders(string apiUrl, string accountID, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/pendingorders?oauth_token=" + accessToken);
            var responsePendingOrders = client.Execute<Deal>(request);
            return JsonConvert.DeserializeObject<List<PendingOrder>>((JObject.Parse(responsePendingOrders.Content)["data"]).ToString());
        }
    }
}