<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_HistoryMark.aspx.cs" Inherits="MyCCare.Admin_CCare.Ad_HistoryMark" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Content" runat="server">
    <div id="menutabs1" class='mt10'>
        <a class="" href="Ad_SubInfo.aspx">
            <img class='icon1' src='../images/icon1.png'><span>Tra cứu thuê bao</span></a>
        <a class="selected" href="Ad_HistoryRegDereg.aspx">
            <img class='icon2' src='../images/icon2.png'><span>Tra cứu sử dụng dịch vụ</span></a>
        <a class="" href="Ad_RegDereg.aspx">
            <img class='icon3' src='../images/icon3.png'><span>Cài đặt dịch vụ</span></a>
        <a class="" href="Ad_ServiceInfo.aspx">
            <img class='icon4' src='../images/icon4.png'><span>Thông tin dịch vụ</span></a>
    </div>
    <div class='tabtracuusddv'>
        <ul>
            <li><a href='Ad_HistoryRegDereg.aspx'>Lịch sử Đăng ký / Hủy</a></li>
            <li><a href='Ad_HistoryCharge.aspx'>Lịch sử trừ cước</a></li>
            <li><a href='Ad_HistoryUsing.aspx'>Lịch sử sử dụng</a></li>
            <li><a href='Ad_HistoryMOMT.aspx' class='wrapping-link-inline '>Lịch sử MO /MT</a></li>
            <li><a href='Ad_HistoryMark.aspx' class='select wrapping-link-inline '>Tra cứu điểm</a></li>
        </ul>
    </div>
    <div class='fillterarea'>
        <table>
            <tr>
                <td width='100'>Số thuê bao:</td>
                <td colspan="5">
                    <input style='width: 147px' type='text' class='textbox' runat="server" id="tbx_MSISDN" /></td>
                <td rowspan='2' align='right' width='200'>
                    <asp:Button runat="server" CssClass="btn_search" ID="btn_Search" Text="Tra cứu" OnClick="btn_Search_Click" /></td>
            </tr>
            <tr>
                <td>Gói cước:</td>
                <td>
                    <select style='width: 150px' class="dropdownlist" runat="server" id="sel_Service">
                        <option>-- Chọn gói cước --</option>
                    </select></td>
                <td>Thời gian bắt đầu:</td>
                <td>
                    <input type="text" class="datetime" runat="server" id="tbx_FromDate" onclick="displayCalendar(this, 'dd/mm/yyyy', this);" /></td>

                <td>Thời gian kết thúc</td>
                <td>
                    <input type="text" class="datetime" runat="server" id="tbx_ToDate" onclick="displayCalendar(this, 'dd/mm/yyyy', this);" /></td>
            </tr>
        </table>
    </div>
    <div class='fillterarea'>
        <table style="line-height:20px;">
            <tr><td colspan="2"><h3>Thông tin điểm hiện tại của Thuê bao</h3></td>
            </tr>
            <tr>
                <td style="width:110px;">Điểm gia hạn:</td>
                <td class="mark"><%=this.ChargeMark.ToString() %></td>
                
            </tr>     
            <tr>
                 <td>Điểm dự đoán:</td>
                <td class="mark">(Điểm dự đoán sẽ được cập nhật vào sáng hôm sau)</td>
            </tr>       
            <tr>
               
                <td>Tổng điểm Tuần:</td>
                <td class="mark"><%=this.WeekMark.ToString() %></td>
            </tr>
        </table>
    </div>
    <table class='tbl_style'>
        <thead>
            <tr>
                <th>Ngày</th>
                <th>Điểm dự đoán Kết Quả</th>
                <th>Điểm dự đoán Bàn Thắng</th>
                <th>Điểm dự đoán Giữ Bóng</th>
                <th>Điểm dự đoán Tỷ Số</th>
                <th>Điểm dự đoán Thẻ Vàng</th>
                <th>Điểm gia hạn</th>
                <th>Tổng điểm ngày</th>
                <th>Tổng điểm tuần</th>
            </tr>
        </thead>
        <asp:Repeater runat="server" ID="rpt_Data">
            <ItemTemplate>
                <tr>
                    <td align='center'><%#Eval("CodeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("CodeDate")).ToString(MyUtility.MyConfig.ShortDateFormat)%></td>
                    <td align='center'><%#Eval("MarkKQ")%></td>
                    <td align='center'><%#Eval("MarkBT")%></td>
                    <td align='center'><%#Eval("MarkGB")%></td>
                    <td align='center'><%#Eval("MarkTS")%></td>
                    <td align='center'><%#Eval("MarkTV")%></td>
                    <td align='center'><%#Eval("ChargeMark")%></td>
                    <td align='center'><%#Eval("DayMark")%></td>
                    <td align='center'><%#Eval("WeekMark")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

    <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
</asp:Content>
