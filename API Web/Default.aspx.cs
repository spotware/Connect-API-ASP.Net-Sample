using Connect_API.Accounts;
using Connect_API.Trading;
using OpenApiLib;
using System;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;

namespace Accounts_API_Web
{
    public partial class _Default : Page
    {
        //private string _clientId = "";
        //private string _clientSecret = "";
        //private string _connectUrl = "https://sandbox-connect.spotware.com/";
        //private string _apiUrl = "https://sandbox-api.spotware.com/";
        //private string _apiHost = "sandbox-tradeapi.spotware.com";

        private string _clientId = "";
        private string _clientSecret = "";
        private string _connectUrl = "https://connect.spotware.com/";
        private string _apiUrl = "https://api.spotware.com/";
        private string _apiHost = "tradeapi.spotware.com";
        private int _apiPort = 5032; 
        private TcpClient _tcpClient = new TcpClient();
        private SslStream _apiSocket;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Token"] == null)
            {
                if (HttpContext.Current.Request.QueryString["code"] != null)
                {
                    var code = HttpContext.Current.Request.QueryString["code"];
                    var redirectUri = Session["Url"].ToString();
                    var token = AccessToken.GetAccessToken(_connectUrl, code, redirectUri, _clientId, _clientSecret);
                    var tokenString = token.Token; 
                    if (tokenString != null)
                    {
                        tcMainContainer.Enabled = true;
                        var profile = Profile.GetProfile(_apiUrl, tokenString);
                        var accounts = TradingAccount.GetTradingAccounts(_apiUrl, tokenString);
                        ddlTradingAccounts.DataSource = accounts;
                        ddlTradingAccounts.DataBind();
                        Session["Token"] = tokenString;
                        Session["RefreshToken"] = token.RefreshToken;
                    }
                }
                else
                {
                    Session["Url"] = HttpContext.Current.Request.Url.AbsoluteUri;
                    Response.Redirect(_connectUrl + "apps/auth?&client_id=" + _clientId + "&redirect_uri=" + HttpContext.Current.Request.Url.AbsoluteUri + "&scope=trading");
                }
            }
            
                _tcpClient = new TcpClient(_apiHost, _apiPort); ;
                _apiSocket = new SslStream(_tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                _apiSocket.AuthenticateAsClient(_apiHost);         
        }
         
        protected void btnTradingAccountDetails_Click(object sender, EventArgs e)
        {
            if (ddlTradingAccounts.SelectedValue != null && Session["Token"] != null)
            {
                var accountID = ddlTradingAccounts.SelectedValue;
                var token = Session["Token"].ToString();
                dgvDeals.DataSource = Deal.GetDeals(_apiUrl, accountID, token);
                dgvDeals.DataBind();
                dgvCashflowHistory.DataSource = CashflowHistory.GetCashflowHistory(_apiUrl, accountID, token);
                dgvCashflowHistory.DataBind();
                dgvPendingOrders.DataSource = PendingOrder.GetPendingOrders(_apiUrl, accountID, token);
                dgvPendingOrders.DataBind();
                dgvPositions.DataSource = Position.GetPositions(_apiUrl, accountID, token);
                dgvPositions.DataBind();
                dgvSymbols.DataSource = Symbols.GetSymbols(_apiUrl, accountID, token);
                dgvSymbols.DataBind();

                var date = new DateTime(2017, 01, 12);
                var tickDataBid = TickData.GetTickData(_apiUrl, accountID, token, "EURUSD", TickData.TickDataType.Bid, date, 07, 13, 46, 07, 15, 26);
                var tickDataAsk = TickData.GetTickData(_apiUrl, accountID, token, "EURUSD", TickData.TickDataType.Ask, date, 07, 13, 46, 07, 15, 26);
                chrTickData.Series[0].Points.Clear();
                foreach (var td in tickDataBid)
                {
                    var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(td.Timestamp) / 1000d));
                    chrTickData.Series[0].Points.Add(new DataPoint(dt.ToOADate(), Convert.ToDouble(td.Tick)));
                }

                chrTickData.Series[1].Points.Clear();
                foreach (var td in tickDataAsk)
                {
                    var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(td.Timestamp) / 1000d));
                    chrTickData.Series[1].Points.Add(new DataPoint(dt.ToOADate(), Convert.ToDouble(td.Tick)));
                }
                chrTickData.ChartAreas[0].AxisY.Minimum = chrTickData.Series[0].Points.Min(x => x.YValues[0]);
                chrTickData.ChartAreas[0].AxisY.Maximum = chrTickData.Series[0].Points.Max(x => x.YValues[0]);

                var dateFrom = new DateTime(2017, 01, 11, 00, 00, 00);
                var dateTo = new DateTime(2017, 01, 13, 00, 00, 00);
                var trendBarH1 = TrendBar.GetTrendBar(_apiUrl, accountID, token, "EURUSD", TrendBar.TrendBarType.Hour, dateFrom, dateTo);

                dateFrom = new DateTime(2017, 01, 12, 23, 00, 00);
                dateTo = new DateTime(2017, 01, 13, 00, 00, 00);
                var trendBarM1 = TrendBar.GetTrendBar(_apiUrl, accountID, token, "EURUSD", TrendBar.TrendBarType.Minute, dateFrom, dateTo);

                chrTrendChartH1.Series[0].Points.Clear();
                foreach (var tb in trendBarH1)
                {
                    var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(tb.Timestamp) / 1000d));
                    chrTrendChartH1.Series[0].Points.Add(new DataPoint(dt.ToOADate(), new double[] { Convert.ToDouble(tb.High), Convert.ToDouble(tb.Low), Convert.ToDouble(tb.Open), Convert.ToDouble(tb.Close) }));
                }
                chrTrendChartH1.ChartAreas[0].AxisY.Minimum = chrTrendChartH1.Series[0].Points.Min(x => x.YValues.Min());
                chrTrendChartH1.ChartAreas[0].AxisY.Maximum = chrTrendChartH1.Series[0].Points.Max(x => x.YValues.Max());

                chrTrendChartM1.Series[0].Points.Clear();
                foreach (var tb in trendBarM1)
                {
                    var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(tb.Timestamp) / 1000d));
                    chrTrendChartM1.Series[0].Points.Add(new DataPoint(dt.ToOADate(), new double[] { Convert.ToDouble(tb.High), Convert.ToDouble(tb.Low), Convert.ToDouble(tb.Open), Convert.ToDouble(tb.Close) }));
                }
                chrTrendChartM1.ChartAreas[0].AxisY.Minimum = chrTrendChartM1.Series[0].Points.Min(x => x.YValues.Min());
                chrTrendChartM1.ChartAreas[0].AxisY.Maximum = chrTrendChartM1.Series[0].Points.Max(x => x.YValues.Max());
            }
        }

        protected void btnCreateDemoAccount_Click(object sender, EventArgs e)
        {
            var token = Session["Token"].ToString();
            var demoAccount = new DemoAccount();
            demoAccount.AccountType = "HEDGED";
            demoAccount.Balance = 10000;
            demoAccount.CountryID = 100;
            demoAccount.DepositCurrency = "EUR";
            demoAccount.Leverage = 500;
            demoAccount.Password = "nsithxcr";
            demoAccount.PhoneNumber = "99999999";
            var result = demoAccount.CreateDemoAccount(_apiUrl, token);
        }

        protected void btnMakeDeposit_Click(object sender, EventArgs e)
        {
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var result = Transactions.Deposit(_apiUrl, token, accountID, txtDepositAmount.Text);
        }

        protected void btnMakeWithdrawal_Click(object sender, EventArgs e)
        {
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var result = Transactions.Withdraw(_apiUrl, token, accountID, txtWithdrawalAmount.Text);
        }

        private byte[] Listen(SslStream apiSocket)
        {
            byte[] _length = new byte[sizeof(int)];
            bool cont = true;
            byte[] _message = new byte[0];
            while (cont)
            {
                int readBytes = 0;
                do
                {
                    readBytes += _apiSocket.Read(_length, readBytes, _length.Length - readBytes);
                } while (readBytes < _length.Length);

                int msgLength = BitConverter.ToInt32(_length.Reverse().ToArray(), 0);

                if (msgLength <= 0)
                    continue;
                cont = false;
                _message = new byte[msgLength];
                readBytes = 0;
                do
                {
                    readBytes += _apiSocket.Read(_message, readBytes, _message.Length - readBytes);
                } while (readBytes < msgLength);
            }

            return _message;
        }

        private void Transmit(OpenApiLib.ProtoMessage msg)
        {          

            var msgByteArray = msg.ToByteArray();
            byte[] length = BitConverter.GetBytes(msgByteArray.Length).Reverse().ToArray();
            _apiSocket.Write(length);
            _apiSocket.Write(msgByteArray);
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            return false;
        }

        protected void btnSendPingRequest_Click(object sender, EventArgs e)
        {
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreatePingRequest(DateTime.Now.Ticks);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendAuthorizationRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
        }

        private void SendAuthorizationRequest()
        {
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateAuthorizationRequest(_clientId, _clientSecret);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSubscribeForTradingEvents_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateSubscribeForTradingEventsRequest(89214, token);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }        

        protected void btnUnsubscribeForTradingEvents_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateUnsubscribeForTradingEventsRequest(Convert.ToInt32(accountID));
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendGetAllSubscriptionsForTradingEventsRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateAllSubscriptionsForTradingEventsRequest();
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendGetAllSubscriptionsForSpotEventsRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateGetAllSpotSubscriptionsRequest();
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendMarketOrderRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateMarketOrderRequest(Convert.ToInt32(accountID), token, "EURUSD", ProtoTradeSide.BUY, Convert.ToInt64(txtOrderVolume.Text));
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
            lblResponse.Text += "<br/>";
            Thread.Sleep(1000);
            _message = Listen(_apiSocket);
            protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text += OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendMarketRangeOrderRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateMarketRangeOrderRequest(Convert.ToInt32(accountID), token, "EURUSD", ProtoTradeSide.BUY, 100000, 1.09, 10);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendLimitOrderRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateLimitOrderRequest(Convert.ToInt32(accountID), token, "EURUSD", ProtoTradeSide.BUY, 100000, 1.09);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendAmendLimitOrderRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateAmendLimitOrderRequest(Convert.ToInt32(accountID), token, 100000, 1.10);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendStopOrderRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateStopOrderRequest(Convert.ToInt32(accountID), token, "EURUSD", ProtoTradeSide.BUY, 1000000, 0.2);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSendClosePositionRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateClosePositionRequest(Convert.ToInt32(accountID), token, 100, 100000);
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnSubscribeForSpotsRequest_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();
            var msgFactory = new OpenApiMessagesFactory();
            var msg = msgFactory.CreateSubscribeForSpotsRequest(Convert.ToInt32(accountID), token, "EURUSD");
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);
        }

        protected void btnRefreshToken_Click(object sender, EventArgs e)
        {
            var token = Session["RefreshToken"].ToString();
            var newToken = AccessToken.RefreshAccessToken(_connectUrl, token, _clientId, _clientSecret);
            
        }

        protected void btnAmendPosition_Click(object sender, EventArgs e)
        {
            SendAuthorizationRequest();
            var accountID = ddlTradingAccounts.SelectedValue;
            var token = Session["Token"].ToString();         
            var PositionId = Convert.ToInt32(txtPositionID.Text);
            var StopLoss = Convert.ToDouble(txtStopLoss.Text);
            var TakeProfit = Convert.ToDouble(txtTakeProfit.Text);
            var msgFactory = new OpenApiMessagesFactory();        
            var _msg = ProtoOAAmendPositionStopLossTakeProfitReq.CreateBuilder();
            _msg.SetAccountId(Convert.ToInt32(accountID));
            _msg.SetAccessToken(token);
            _msg.SetPositionId(PositionId);
            _msg.SetStopLossPrice(StopLoss);
            _msg.SetTakeProfitPrice(TakeProfit);
            var msg = msgFactory.CreateMessage((uint)_msg.PayloadType, _msg.Build().ToByteString(), PositionId.ToString());
            Transmit(msg);
            byte[] _message = Listen(_apiSocket);
            var protoMessage = msgFactory.GetMessage(_message);
            lblResponse.Text = OpenApiMessagesPresentation.ToString(protoMessage);

        }
    }
}