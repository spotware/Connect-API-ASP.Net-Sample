using Newtonsoft.Json;

namespace Connect_API.Accounts
{
    /// <summary>
    ///  The PositionCloseDetails class is used for retrieving position close details for a specified position using the Connect API RESTful web services.
    /// </summary>
    public class PositionCloseDetails
    {
        /// <summary>
        /// Gets or sets the entry price.
        /// </summary>
        /// <value>
        /// The entry price.
        /// </value>
        [JsonProperty("entryPrice")]
        public string EntryPrice { get; set; }

        /// <summary>
        /// Gets or sets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        [JsonProperty("profit")]
        public string Profit { get; set; }

        /// <summary>
        /// Gets or sets the swap.
        /// </summary>
        /// <value>
        /// The swap.
        /// </value>
        [JsonProperty("swap")]
        public string Swap { get; set; }

        /// <summary>
        /// Gets or sets the commission.
        /// </summary>
        /// <value>
        /// The commission.
        /// </value>
        [JsonProperty("commission")]
        public string Commission { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("balanceVersion")]
        public string BalanceVersion { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("stopLossPrice")]
        public string StopLossPrice { get; set; }

        /// <summary>
        /// Gets or sets the take profit price.
        /// </summary>
        /// <value>
        /// The take profit price.
        /// </value>
        [JsonProperty("takeProfitPrice")]
        public string TakeProfitPrice { get; set; }

        /// <summary>
        /// Gets or sets the quote to deposit conversion rate.
        /// </summary>
        /// <value>
        /// The quote to deposit conversion rate.
        /// </value>
        [JsonProperty("quoteToDepositConversionRate ")]
        public string QuoteToDepositConversionRate { get; set; }

        /// <summary>
        /// Gets or sets the closed to deposit conversion rate.
        /// </summary>
        /// <value>
        /// The closed to deposit conversion rate.
        /// </value>
        [JsonProperty("closedToDepositConversionRate")]
        public string ClosedToDepositConversionRate { get; set; }

        /// <summary>
        /// Gets or sets the closed volume.
        /// </summary>
        /// <value>
        /// The closed volume.
        /// </value>
        [JsonProperty("closedVolume")]
        public string ClosedVolume { get; set; }

        /// <summary>
        /// Gets or sets the profit in pips.
        /// </summary>
        /// <value>
        /// The profit in pips.
        /// </value>
        [JsonProperty("profitInPips")]
        public string ProfitInPips { get; set; }
    }
}