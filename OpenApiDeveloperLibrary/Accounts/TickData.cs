using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Connect_API.Accounts
{
    /// <summary>
    ///  The TickData class is used for retrieving tick data for a specified account using the Connect API RESTful web services.
    /// </summary>
    public class TickData
    {
        /// <summary>
        /// The Tick Data Type
        /// </summary>
        public enum TickDataType
        {
            Bid,
            Ask
        }

        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// The tick
        /// </summary>
        [JsonProperty("tick")]
        public string Tick { get; set; }

        /// <summary>
        /// Gets the tick data.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accountID">The account identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="tickDataType">Type of the tick data.</param>
        /// <param name="date">The date.</param>
        /// <param name="hourFrom">The hour from.</param>
        /// <param name="minuteFrom">The minute from.</param>
        /// <param name="secondFrom">The second from.</param>
        /// <param name="hourTo">The hour to.</param>
        /// <param name="minuteTo">The minute to.</param>
        /// <param name="secondTo">The second to.</param>
        /// <returns></returns>
        public static List<TickData> GetTickData(string apiUrl, string accountID, string accessToken, string symbol, TickDataType tickDataType, DateTime date, int hourFrom, int minuteFrom, int secondFrom, int hourTo, int minuteTo, int secondTo)
        {
            string tickDataTypeString = string.Empty;
            switch (tickDataType)
            {
                case TickDataType.Bid:
                    tickDataTypeString = "bid";
                    break;

                case TickDataType.Ask:
                    tickDataTypeString = "ask";
                    break;
            }

            string dateString = String.Format("{0:0000}", date.Year) + String.Format("{0:00}", date.Month) + String.Format("{0:00}", date.Day);
            string timeFrom = String.Format("{0:00}", hourFrom) + String.Format("{0:00}", minuteFrom) + String.Format("{0:00}", secondFrom);
            string timeTo = String.Format("{0:00}", hourTo) + String.Format("{0:00}", minuteTo) + String.Format("{0:00}", secondTo);
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"connect/tradingaccounts/" + accountID + "/symbols/" + symbol + "/" + tickDataTypeString + "/?oauth_token=" + accessToken + "&date=" + dateString + "&from=" + timeFrom + "&to=" + timeTo);
            var responseTickDataBid = client.Execute<TickData>(request);
            return JsonConvert.DeserializeObject<List<TickData>>((JObject.Parse(responseTickDataBid.Content)["data"]).ToString());
        }
    }
}