using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    ///  The position class is used for retrieving positions for a specified account using the Connect API RESTful web services.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        [JsonProperty("positionID")]
        public string PositionID { get; set; }

        /// <summary>
        /// Gets or sets the entry timestamp.
        /// </summary>
        /// <value>
        /// The entry timestamp.
        /// </value>
        [JsonProperty("entryTimestamp")]
        public string EntryTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the ut clast update timestamp.
        /// </summary>
        /// <value>
        /// The ut clast update timestamp.
        /// </value>
        [JsonProperty("utcLastUpdateTimestamp")]
        public string UTClastUpdateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the symbol.
        /// </summary>
        /// <value>
        /// The name of the symbol.
        /// </value>
        [JsonProperty("symbolName")]
        public string SymbolName { get; set; }

        /// <summary>
        /// Gets or sets the trade side.
        /// </summary>
        /// <value>
        /// The trade side.
        /// </value>
        [JsonProperty("tradeSide")]
        public string TradeSide { get; set; }

        /// <summary>
        /// Gets or sets the entry price.
        /// </summary>
        /// <value>
        /// The entry price.
        /// </value>
        [JsonProperty("entryPrice")]
        public string EntryPrice { get; set; }

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
        /// Gets or sets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        [JsonProperty("profit")]
        public string Profit { get; set; }

        /// <summary>
        /// Gets or sets the profit in pips.
        /// </summary>
        /// <value>
        /// The profit in pips.
        /// </value>
        [JsonProperty("profitInPips")]
        public string ProfitInPips { get; set; }

        /// <summary>
        /// Gets or sets the commission.
        /// </summary>
        /// <value>
        /// The commission.
        /// </value>
        [JsonProperty("commission")]
        public string Commission { get; set; }

        /// <summary>
        /// Gets or sets the swap.
        /// </summary>
        /// <value>
        /// The swap.
        /// </value>
        [JsonProperty("swap")]
        public string Swap { get; set; }

        /// <summary>
        /// Gets or sets the current price.
        /// </summary>
        /// <value>
        /// The current price.
        /// </value>
        [JsonProperty("currentPrice")]
        public string CurrentPrice { get; set; }

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
        /// Gets or sets the test partner.
        /// </summary>
        /// <value>
        /// The test partner.
        /// </value>
        [JsonProperty("testPartner")]
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
        /// Gets the positions.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static List<Position> GetPositions(string apiUrl, string accountID, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/positions?oauth_token=" + accessToken);
            var responsePosition = client.Execute<Position>(request);
            return JsonConvert.DeserializeObject<List<Position>>((JObject.Parse(responsePosition.Content)["data"]).ToString());
        }
    }
}