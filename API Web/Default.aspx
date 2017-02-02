<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Accounts_API_Web._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:DropDownList ID="ddlTradingAccounts" DataValueField="AccountID" DataTextField="AccountNumber" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnTradingAccountDetails" runat="server" Text="Show Trading Account Details" OnClick="btnTradingAccountDetails_Click" />
    <br />
    <asp:Button ID="btnRefreshToken" runat="server" Text="Refresh Token" OnClick="btnRefreshToken_Click" />
    <ajaxToolkit:TabContainer ID="tcMainContainer" runat="server" ActiveTabIndex="0" Enabled="False">
        <ajaxToolkit:TabPanel runat="server" HeaderText="Deals" ID="tpDeals">
            <ContentTemplate>
                <asp:GridView ID="dgvDeals" runat="server"></asp:GridView>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Cashflow History" ID="tpCashflowHistory">
            <ContentTemplate>
                <asp:GridView ID="dgvCashflowHistory" runat="server"></asp:GridView>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Pending Orders" ID="tpPendingOrders">
            <ContentTemplate>
                <asp:GridView ID="dgvPendingOrders" runat="server"></asp:GridView>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Positions" ID="tpPositions">
            <ContentTemplate>
                <asp:GridView ID="dgvPositions" runat="server"></asp:GridView>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Symbols" ID="tpSymbols">
            <ContentTemplate>
                <asp:GridView ID="dgvSymbols" runat="server"></asp:GridView>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Tick Data" ID="tpTickData">
            <ContentTemplate>
                <asp:Chart ID="chrTickData" runat="server" Width="1200">
                    <Series>
                        <asp:Series Name="Bid" ChartType="Line" XValueType="Date">
                        </asp:Series>
                    </Series>
                    <Series>
                        <asp:Series Name="Ask" ChartType="Line" XValueType="Date">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="caTickData"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Trend Bars" ID="tpTrendBars">
            <ContentTemplate>
                <ajaxToolkit:TabContainer ID="tcTrendData" runat="server" ActiveTabIndex="0">
                    <ajaxToolkit:TabPanel runat="server" HeaderText="By Hour" ID="TabPanel1">
                        <ContentTemplate>
                            <asp:Chart ID="chrTrendChartH1" runat="server" Width="1200">
                                <Series>
                                    <asp:Series Name="Hour" ChartType="Candlestick" XValueType="Date">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="caTrendBarHour"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" HeaderText="By Minute" ID="TabPanel2">
                        <ContentTemplate>
                            <asp:Chart ID="chrTrendChartM1" runat="server" Width="1200">
                                <Series>
                                    <asp:Series Name="Mimute" ChartType="Candlestick" XValueType="Date">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="caTrendBarMinute"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Demo Account Creation" ID="tpDemoAccountCreation">
            <ContentTemplate>
                <asp:Button ID="btnCreateDemoAccount" runat="server" Text="Create Demo Account" OnClick="btnCreateDemoAccount_Click" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Deposit" ID="tpDeposit">
            <ContentTemplate>
                <asp:TextBox ID="txtDepositAmount" TextMode="Number" runat="server"></asp:TextBox>
                <asp:Button ID="btnMakeDeposit" runat="server" Text="Make Deposit to Account" OnClick="btnMakeDeposit_Click" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Withdraw" ID="tpWithdraw">
            <ContentTemplate>
                <asp:TextBox ID="txtWithdrawalAmount" TextMode="Number" runat="server"></asp:TextBox>
                <asp:Button ID="btnMakeWithdrawal" runat="server" Text="Make Withdrawal from Account" OnClick="btnMakeWithdrawal_Click" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Trading" ID="tpTrading" >
            <ContentTemplate>
                <asp:Button ID="btnSendPingRequest" runat="server" Text="Send Ping Request" OnClick="btnSendPingRequest_Click" />
                <br />
                <asp:Button ID="btnSendAuthorizationRequest" runat="server" Text="Send Authorization Request" OnClick="btnSendAuthorizationRequest_Click" />
                <br />
                <asp:Button ID="btnSubscribeForTradingEvents" runat="server" Text="Subscribe for Trading Events" OnClick="btnSubscribeForTradingEvents_Click" />
                <br />
                <asp:Button ID="btnUnsubscribeForTradingEvents" runat="server" Text="Unubscribe for Trading Events" OnClick="btnUnsubscribeForTradingEvents_Click" />
                <br />
                <asp:Button ID="btnSendGetAllSubscriptionsForTradingEventsRequest" runat="server" Text="Get All Subscriptions for Trading Events" OnClick="btnSendGetAllSubscriptionsForTradingEventsRequest_Click" />
                <br />
                <asp:Button ID="btnSubscribeForSpotsRequest" runat="server" Text="Subscribe for Spots" OnClick="btnSubscribeForSpotsRequest_Click" />
                <br />
                <asp:Button ID="btnSendGetAllSubscriptionsForSpotEventsRequest" runat="server" Text="Get All Subscriptions for Spot Events" OnClick="btnSendGetAllSubscriptionsForSpotEventsRequest_Click" />
                <br />
                <asp:Button ID="btnSendMarketOrderRequest" runat="server" Text="Send Market Order" OnClick="btnSendMarketOrderRequest_Click" />
                <br />
                <asp:Button ID="btnSendMarketRangeOrderRequest" runat="server" Text="Send Market Range Order" OnClick="btnSendMarketRangeOrderRequest_Click" />
                <br />
                <asp:Button ID="btnSendLimitOrderRequest" runat="server" Text="Send Limit Order" OnClick="btnSendLimitOrderRequest_Click" />
                <br />
                <asp:Button ID="btnSendAmendLimitOrderRequest" runat="server" Text="Amend Limit Order" OnClick="btnSendAmendLimitOrderRequest_Click" />
                <br />
                <asp:Button ID="btnSendStopOrderRequest" runat="server" Text="Send Stop Order" OnClick="btnSendStopOrderRequest_Click" />
                <br />
                <asp:Button ID="btnSendClosePositionRequest" runat="server" Text="Send Close Position" OnClick="btnSendClosePositionRequest_Click" />
                <br />
                   <asp:Button ID="btnAmendPosition" runat="server" Text="Modify Stop Loss/Take Profit" OnClick="btnAmendPosition_Click" />
                   <asp:Label ID="lblPositionID" runat="server" Text="Label">Position ID</asp:Label><asp:TextBox ID="txtPositionID" runat="server"></asp:TextBox>
                     <asp:Label ID="lblTakeProfit" runat="server" Text="Label">Take Profit</asp:Label><asp:TextBox ID="txtTakeProfit" runat="server"></asp:TextBox>
                   <asp:Label ID="lblStopLoss" runat="server" Text="Label">Stop Loss</asp:Label><asp:TextBox ID="txtStopLoss" runat="server"></asp:TextBox>
              <br />
                <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>