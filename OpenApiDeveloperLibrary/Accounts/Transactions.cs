using RestSharp;

namespace Connect_API.Accounts
{
    /// <summary>
    /// Transaction class contains functions for executing transactions. 
    /// </summary>
    public class Transactions
    {
        /// <summary>
        /// Deposits the specified amount to the specified account.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static string Deposit(string apiUrl, string accessToken, string accountId, string amount)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"/connect/tradingaccounts/" + accountId + "/deposit/?oauth_token=" + accessToken, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { amount = amount });
            return client.Execute(request).StatusDescription;
        }

        /// <summary>
        /// Withdraws the specified amount from the specifiend account.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static string Withdraw(string apiUrl, string accessToken, string accountId, string amount)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(@"/connect/tradingaccounts/" + accountId + "/withdraw/?oauth_token=" + accessToken, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { amount = amount });
            return client.Execute(request).StatusDescription;
        }
    }
}