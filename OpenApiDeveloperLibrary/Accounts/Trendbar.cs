using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    ///  The TrendBar class is used for retrieving trend bar for a specified account using the Connect API RESTful web services.
    /// </summary>
    public class TrendBar
    {
        /// <summary>
        /// The Trend Bar Type
        /// </summary>
        public enum TrendBarType
        {
            Hour,
            Minute
        }

        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// The high price
        /// </summary>
        [JsonProperty("high")]
        public string High { get; set; }

        /// <summary>
        /// The low price
        /// </summary>
        [JsonProperty("low")]
        public string Low { get; set; }

        /// <summary>
        /// The open price
        /// </summary>
        [JsonProperty("open")]
        public string Open { get; set; }

        /// <summary>
        /// The close price
        /// </summary>
        [JsonProperty("close")]
        public string Close { get; set; }

        /// <summary>
        /// The volume price
        /// </summary>
        [JsonProperty("volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets the trend bar.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="trendBarType">Type of the trend bar.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns></returns>
        public static List<TrendBar> GetTrendBar(string apiUrl, string accountID, string accessToken, string symbol, TrendBarType trendBarType, DateTime dateFrom, DateTime dateTo)
        {
            string trendBarTypeString = string.Empty;
            switch (trendBarType)
            {
                case TrendBarType.Hour:
                    trendBarTypeString = "H1";
                    break;

                case TrendBarType.Minute:
                    trendBarTypeString = "M1";
                    break;
            }

            string dateFromString = String.Format("{0:0000}", dateFrom.Year) + String.Format("{0:00}", dateFrom.Month) + String.Format("{0:00}", dateFrom.Day) + String.Format("{0:00}", dateFrom.Hour) + String.Format("{0:00}", dateFrom.Minute) + String.Format("{0:00}", dateFrom.Second);
            string dateToString = String.Format("{0:0000}", dateTo.Year) + String.Format("{0:00}", dateTo.Month) + String.Format("{0:00}", dateTo.Day) + String.Format("{0:00}", dateTo.Hour) + String.Format("{0:00}", dateTo.Minute) + String.Format("{0:00}", dateTo.Second);
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/symbols/" + symbol + "/trendbars/" + trendBarTypeString + "/?oauth_token=" + accessToken + "&from=" + dateFromString + "&to=" + dateToString);
            var responseTrendBarH1 = client.Execute<TrendBar>(request);
            return JsonConvert.DeserializeObject<List<TrendBar>>((JObject.Parse(responseTrendBarH1.Content)["data"]).ToString());
        }
    }
}