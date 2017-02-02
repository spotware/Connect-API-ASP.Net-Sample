using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    ///  The pending order class is used for retrieving deals for symbols using the Connect API RESTful web services.
    /// </summary>
    public class Symbols
    {
        /// <summary>
        /// Gets or sets the name of the symbol.
        /// </summary>
        /// <value>
        /// The name of the symbol.
        /// </value>
        [JsonProperty("symbolName")]
        public string SymbolName { get; set; }

        /// <summary>
        /// Gets or sets the digits.
        /// </summary>
        /// <value>
        /// The digits.
        /// </value>
        [JsonProperty("digits")]
        public string Digits { get; set; }

        /// <summary>
        /// Gets or sets the pip position.
        /// </summary>
        /// <value>
        /// The pip position.
        /// </value>
        [JsonProperty("pipPosition")]
        public string PipPosition { get; set; }

        /// <summary>
        /// Gets or sets the measurement units.
        /// </summary>
        /// <value>
        /// The measurement units.
        /// </value>
        [JsonProperty("measurementUnits")]
        public string MeasurementUnits { get; set; }

        /// <summary>
        /// Gets or sets the base asset.
        /// </summary>
        /// <value>
        /// The base asset.
        /// </value>
        [JsonProperty("baseAsset")]
        public string BaseAsset { get; set; }

        /// <summary>
        /// Gets or sets the quote asset.
        /// </summary>
        /// <value>
        /// The quote asset.
        /// </value>
        [JsonProperty("quoteAsset")]
        public string QuoteAsset { get; set; }

        /// <summary>
        /// Gets or sets the trade enabled.
        /// </summary>
        /// <value>
        /// The trade enabled.
        /// </value>
        [JsonProperty("tradeEnabled")]
        public string TradeEnabled { get; set; }

        /// <summary>
        /// Gets or sets the size of the tick.
        /// </summary>
        /// <value>
        /// The size of the tick.
        /// </value>
        [JsonProperty("tickSize")]
        public string TickSize { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the maximum leverage.
        /// </summary>
        /// <value>
        /// The maximum leverage.
        /// </value>
        [JsonProperty("maxLeverage")]
        public string MaxLeverage { get; set; }

        /// <summary>
        /// Gets or sets the swap long.
        /// </summary>
        /// <value>
        /// The swap long.
        /// </value>
        [JsonProperty("swapLong")]
        public string SwapLong { get; set; }

        /// <summary>
        /// Gets or sets the swap short.
        /// </summary>
        /// <value>
        /// The swap short.
        /// </value>
        [JsonProperty("swapShort")]
        public string SwapShort { get; set; }

        /// <summary>
        /// Gets or sets the three days swaps.
        /// </summary>
        /// <value>
        /// The three days swaps.
        /// </value>
        [JsonProperty("threeDaysSwaps")]
        public string ThreeDaysSwaps { get; set; }

        /// <summary>
        /// Gets or sets the minimum order volume.
        /// </summary>
        /// <value>
        /// The minimum order volume.
        /// </value>
        [JsonProperty("minOrderVolume")]
        public string MinOrderVolume { get; set; }

        /// <summary>
        /// Gets or sets the minimum order step.
        /// </summary>
        /// <value>
        /// The minimum order step.
        /// </value>
        [JsonProperty("minOrderStep")]
        public string MinOrderStep { get; set; }

        /// <summary>
        /// Gets or sets the maximum order volume.
        /// </summary>
        /// <value>
        /// The maximum order volume.
        /// </value>
        [JsonProperty("maxOrderVolume")]
        public string MaxOrderVolume { get; set; }

        /// <summary>
        /// Gets or sets the asset class.
        /// </summary>
        /// <value>
        /// The asset class.
        /// </value>
        [JsonProperty("assetClass")]
        public string AssetClass { get; set; }

        [JsonProperty("lastBid")]
        public string LastBid { get; set; }

        /// <summary>
        /// Gets or sets the last ask.
        /// </summary>
        /// <value>
        /// The last ask.
        /// </value>
        [JsonProperty("lastAsk")]
        public string LastAsk { get; set; }

        /// <summary>
        /// Gets or sets the trading mode.
        /// </summary>
        /// <value>
        /// The trading mode.
        /// </value>
        [JsonProperty("tradingMode")]
        public string TradingMode { get; set; }

        /// <summary>
        /// Gets the symbols.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static List<Symbols> GetSymbols(string apiUrl, string accountID, string accessToken)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/symbols?oauth_token=" + accessToken);
            var responseSymbols = client.Execute<Symbols>(request);
            return JsonConvert.DeserializeObject<List<Symbols>>((JObject.Parse(responseSymbols.Content)["data"]).ToString());
        }
    }
}