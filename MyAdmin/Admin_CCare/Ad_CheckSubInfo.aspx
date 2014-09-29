<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_CheckSubInfo.aspx.cs" Inherits="MyAdmin.Admin_CCare.Ad_CheckSubInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
    <style type="text/css">
        .label
        {
            font-weight:bold;
            text-align:center;
            font-size:18px;
            color:#0192DD;
            padding:10px 0 5px 0;
            border-bottom-style: solid;
	        border-bottom-width: 1px;
	        border-bottom-color: #0192DD;
        }
        .info
        {
            width: 290px;
            float: left;
            margin-right: 15px;
        }
        .value
        {
            display: inline-block;
            background-color: #F6F6F6;
            border-bottom-style: solid;
            border-right-style: solid;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-right-color: #E9E9E9;
            border-bottom-color: #E9E9E9;
            font-weight: bold;
            color: #DD0000;
            text-align: right;
            padding: 5px 0 5px 5px;
            height: 14px;
            margin-bottom: 2px;
            padding-right: 5px;
            width: 120px;
            text-align: left;
        }
        
        .title
        {
            display: inline-block;
            width: 150px;
            background-color: #F6F6F6;
            border-bottom-style: solid;
            border-right-style: solid;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-right-color: #E9E9E9;
            border-bottom-color: #E9E9E9;
            font-weight: bold;
            color: #666666;
            text-align: right;
            padding: 5px 0 5px 0;
            height: 14px;
            margin-bottom: 2px;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <label>
        Số điện thoại
    </label>
    <input type="text" runat="server" id="tbx_MSISDN" />
    <asp:Button runat="server" ID="tbx_Search" Text="Tra cứu" OnClick="tbx_Search_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
    <div class="Search">
        <div class="Search_T">
            <div class="Search_TL">
                <div class="Search_TR">
                </div>
            </div>
        </div>
        <div class="Search_ML">
            <div class="Search_MR">
                <div class="Search_MC">
                    <div class="label ">
                        THÔNG TIN SỬ DỤNG DỊCH VỤ CHO THUÊ BAO: <%= mSubInfo.MSISDN %>
                    </div>
                    <div class="NewLine_Pad" style="margin-top:10px;">
                        <fieldset class="info">
                            <legend>Thông tin cơ bản</legend>                          
                            <div>
                                <div class="title">
                                    Tình trạng:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.StatusName %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Đăng ký lần đầu:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.FirstDate %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Đăng ký lần cuối:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.EffectiveDate %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Ngày hết hạn:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.ExpiryDate %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Gia hạn gần nhất:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.ChargeDate %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Retry gần nhất:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.RetryChargeDate %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Hủy lần cuối:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.DeregDate != "NULL" ? mSubInfo.DeregDate : mSubInfo.CofirmDeregDate%></div>
                            </div>
                            <div>
                                <div class="title">
                                    Kênh đăng ký/Hủy:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.ChannelTypeName %></div>
                            </div>
                        </fieldset>
                        <fieldset class="info">
                            <legend>Thông tin dự đoán</legend>
                            <div>
                                <div class="title">
                                    Số MO dự đoán trong ngày:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.MOByDay %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Điểm charge gần nhất:</div>
                                <div class="value">
                                    <%= mSubInfo.ChargeMark %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Điểm tuần gần nhất:</div>
                                <div class="value">
                                    <%= mSubInfo.WeekMark %></div>
                            </div>
                            <div>
                                <div class="title">
                                    Dự đoán Kết quả:</div>
                                <div class="value"><%= mSubInfo.AnswerKQ %>
                                </div>
                            </div>
                            <div>
                                <div class="title">
                                    Dự đoán Bàn thắng:</div>
                                <div class="value"><%= mSubInfo.AnswerBT %>
                                </div>
                            </div>
                            <div>
                                <div class="title">
                                    Dự đoán Tỷ số:</div>
                                <div class="value"><%= mSubInfo.AnswerTS %>
                                </div>
                            </div>
                            <div>
                                <div class="title">
                                    Dự đoán Giữ bóng:</div>
                                <div class="value"><%= mSubInfo.AnswerGB %>
                                </div>
                            </div>
                            <div>
                                <div class="title">
                                    Dự đoán Thẻ vàng:</div>
                                <div class="value"><%= mSubInfo.AnswerTV %>
                                </div>
                            </div>
                        </fieldset>
                          <fieldset class="info">
                            <legend>Thông tin khác</legend>
                            <div>
                                <div class="title">
                                    Appication API:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.AppName %></div>
                            </div>
                              <div>
                                <div class="title">
                                    UserName API:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.UserName %></div>
                            </div>
                            <div>
                                <div class="title">
                                    IP Address API:
                                </div>
                                <div class="value">
                                    <%= mSubInfo.IP %></div>
                            </div>
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
        <div class="Search_B">
            <div class="Search_BL">
                <div class="Search_BR">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
</asp:Content>
