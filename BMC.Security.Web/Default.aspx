<%@ Page Async="true" Title="BMC Security 0.1" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BMC.Security.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Panel ID="PassPanel" runat="server">
                    <table class="uk-table">
                        <tr>
                            <td colspan="3">Welcome to BMC Security Panel 0.1
                            </td>
                        </tr>
                        <tr>
                            <td>PASSCODE</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox CssClass="uk-input" TextMode="Password" ID="TxtPass" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button CssClass="uk-button uk-button-default" ID="BtnPass" runat="server" Text="Enter" /></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ForeColor="Red" Font-Bold="true" runat="server" ID="TxtLogin"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="ControlPanel" Visible="false" runat="server">
                    <h2>BMC Security Control 0.1</h2>
                    <table class="uk-table">
                        <thead>
                            <tr>
                                <th class="uk-table-shrink">No</th>
                                <th class="uk-width-small">Description</th>
                                <th class="uk-table-expand">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Keluarkan Suara Monster</td>
                                <td>
                                    <asp:Button CssClass="uk-button uk-button-default" CommandName="Monster" ID="BtnMonster" runat="server" Text="Play" /></td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Keluarkan Suara Tornado Sirine</td>
                                <td>
                                    <asp:Button CssClass="uk-button uk-button-default" CommandName="Tornado" ID="BtnTornado" runat="server" Text="Play" /></td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Keluarkan Suara Polisi</td>
                                <td>
                                    <asp:Button CssClass="uk-button uk-button-default" CommandName="Police" ID="BtnPolice" runat="server" Text="Play" /></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>Keluarkan Suara Teriakan</td>
                                <td>
                                    <asp:Button CssClass="uk-button uk-button-default" CommandName="Scream" ID="BtnScream" runat="server" Text="Play" /></td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td>Nyalakan LED</td>
                                <td>
                                    <asp:Button CssClass="uk-button uk-button-default" CommandName="LEDON" ID="BtnLedOn" runat="server" Text="Turn On" /></td>
                            </tr>
                            <tr>
                                <td>6</td>
                                <td>Matikan LED</td>
                                <td>
                                    <asp:Button CssClass="uk-button uk-button-default" CommandName="LEDOFF" ID="BtnLedOff" runat="server" Text="Turn Off" /></td>
                            </tr>
                            <tr>
                                <td>7</td>
                                <td>CCTV Watcher Status</td>
                                <td>
                                    <div class="uk-button-group">
                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="CCTVStatus" CommandArgument="True" ID="BtnCCTVOn" runat="server" Text="Turn On" />
                                        <asp:Button CssClass="uk-button uk-button-danger " CommandName="CCTVStatus" CommandArgument="False" ID="BtnCCTVOff" runat="server" Text="Turn Off" />
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>8</td>
                                <td>CCTV Watcher Update Time</td>
                                <td>
                                    <table class="uk-table">
                                        <tr>
                                            <td>Interval : </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtInterval" CssClass="uk-input" TextMode="Number" runat="server"></asp:TextBox>
                                                Detik</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button CssClass="uk-button uk-button-default" CommandName="CCTVUpdateTime" ID="BtnCCTVInterval" runat="server" Text="Set Waktu" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>9</td>
                                <td>Hidroponik</td>
                                <td>

                                    <div class="uk-button-group">
                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="Relay1" CommandArgument="True" ID="BtnRelay1" runat="server" Text="Relay 1 On" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="Relay1" CommandArgument="False" ID="BtnRelay1Off" runat="server" Text="Relay 1 Off" />
                                    </div>

                                    <div class="uk-button-group">

                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="Relay2" CommandArgument="True" ID="BtnRelay2" runat="server" Text="Relay 2 On" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="Relay2" CommandArgument="False" ID="BtnRelay2Off" runat="server" Text="Relay 2 Off" />

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>10</td>
                                <td>Aquascape</td>
                                <td>
                                    <div class="uk-button-group">
                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="WaterIn" CommandArgument="http://192.168.0.99/PIN0_ON" ID="WaterInBtn1" runat="server" Text="Water In Start" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="WaterIn" CommandArgument="http://192.168.0.99/PIN0_OFF" ID="WaterInBtn2" runat="server" Text="Water In Stop" />
                                    </div>
                                    <div class="uk-button-group">

                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="WaterOut" CommandArgument="http://192.168.0.99/PIN1_ON" ID="WaterOutBtn1" runat="server" Text="Water Out Start" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="WaterOut" CommandArgument="http://192.168.0.99/PIN1_OFF" ID="WaterOutBtn2" runat="server" Text="Water Out Stop" />

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>11</td>
                                <td>Aquaponic</td>
                                <td>

                                    <div class="uk-button-group">
                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="RelayAqua1" CommandArgument="True" ID="BtnRelayAqua1" runat="server" Text="Relay 1 On" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="RelayAqua1" CommandArgument="False" ID="BtnRelayAqua1Off" runat="server" Text="Relay 1 Off" />
                                    </div>

                                    <div class="uk-button-group">

                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="RelayAqua2" CommandArgument="True" ID="BtnRelayAqua2" runat="server" Text="Relay 2 On" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="RelayAqua2" CommandArgument="False" ID="BtnRelayAqua2Off" runat="server" Text="Relay 2 Off" />

                                    </div>
                                </td>
                            </tr>
                            <%-- Automation Drainage --%>
                            <tr>
                                <td>12</td>
                                <td>Automation Drainage</td>
                                <td>
                                    <div>
                                        <asp:CheckBox ID="DrainRelay1" runat="server" Text="Bucket 1" />
                                        <asp:CheckBox ID="DrainRelay2" runat="server" Text="Bucket 2" />
                                        <asp:CheckBox ID="DrainRelay3" runat="server" Text="Bucket 3" />
                                        <asp:CheckBox ID="DrainRelay4" runat="server" Text="Bucket 4" />
                                        <asp:CheckBox ID="DrainRelay5" runat="server" Text="Bucket 5" />
                                        <asp:CheckBox ID="DrainRelay6" runat="server" Text="Bucket 6" />
                                        <asp:CheckBox ID="DrainRelay8" runat="server" Text="Bucket 8" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="DrainRelay7" runat="server" Text="Bucket 7" />
                                        <asp:CheckBox ID="DrainRelay9" runat="server" Text="Bucket 9" />
                                        <asp:CheckBox ID="DrainRelay10" runat="server" Text="Bucket 10" />
                                        <asp:CheckBox ID="DrainRelay11" runat="server" Text="Bucket 11" />
                                        <asp:CheckBox ID="DrainRelay12" runat="server" Text="Bucket 12" />
                                        <asp:CheckBox ID="DrainRelay13" runat="server" Text="Bucket 13" />
                                        <asp:CheckBox ID="DrainRelay14" runat="server" Text="Bucket 14" />
                                    </div>
                                    <div></div>
                                    <div class="uk-button-group">
                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="DraineageOn" CommandArgument="True" ID="BtnDrainageOn" runat="server" Text="Drain On" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="DraineageOff" CommandArgument="False" ID="BtnDrainageOff" runat="server" Text="Drain Off" />
                                    </div>
                                    <div class="uk-button-group">
                                        <asp:Button CssClass="uk-button uk-button-primary" CommandName="DraineageFillOn" CommandArgument="False" ID="BtnDrainageFillOn" runat="server" Text="Fill Water On" />
                                        <asp:Button CssClass="uk-button uk-button-danger" CommandName="DraineageFillOff" CommandArgument="False" ID="BtnDrainageFillOff" runat="server" Text="Fill Water Off" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="uk-table">
                        <thead>
                            <tr>
                                <th class="uk-table-shrink">No</th>
                                <th class="uk-width-small">Device Name</th>
                                <th class="uk-table-expand">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="RptControlDevice">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ID") %></td>
                                        <td><%# Eval("Name") %></td>
                                        <td>
                                            <div class="uk-button-group">

                                                <asp:Button CssClass="uk-button uk-button-primary" OnClick="DoAction" CommandName="DEVICEON" CommandArgument='<%#Eval("IP") %>' runat="server" Text="On" />
                                                <asp:Button CssClass="uk-button uk-button-danger" OnClick="DoAction" CommandName="DEVICEOFF" CommandArgument='<%#Eval("IP") %>' runat="server" Text="Off" />

                                            </div>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>

                    </table>
                    <div class="well">
                        <asp:Label ID="TxtStatus" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <p>Loading...</p>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
