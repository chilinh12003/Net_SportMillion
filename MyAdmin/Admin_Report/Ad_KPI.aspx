<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_KPI.aspx.cs" Inherits="MyAdmin.Admin_Report.Ad_KPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
  <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
   <label>
        Từ ngày:</label>
    <input type="text" runat="server" id="tbx_FromDate" style="width: 70px;" />
    <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_FromDate.ClientID %>'),'dd/mm/yyyy',this)" />
    <label>
        Đến ngày:</label>
    <input type="text" runat="server" id="tbx_ToDate" style="width: 70px;" />
    <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_ToDate.ClientID %>'),'dd/mm/yyyy',this)" />
    <label>
        Loại KPI
    </label>
    <select runat="server" id="sel_KPIType">
    </select>
    <asp:Button runat="server" ID="btn_Execute" OnClick="btn_Execute_Click" Text="Thống kê" />
    <div runat="server" id="div_MO" style="font-size: 12px;" visible="false">
        <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ xử lý MO</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau">100%</label>
            </p>
            <p>
                <label>
                    Tổng MO:</label><label class="DanhDau"><%=Total.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng MO đã xử lý:</label><label class="DanhDau"><%=Total.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
        </fieldset>
        <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ MO thành công</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau"><%=percent.ToString(MyUtility.MyConfig.DoubleFormat)%>%</label>
            </p>
            <p>
                <label>
                    Tổng MO:</label><label class="DanhDau"><%=Total.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng MO thành công:</label><label class="DanhDau"><%=TotalSuccess.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
        </fieldset>
    </div>
    <div runat="server" id="div_MT" style="font-size: 12px;" visible="false">
        <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ MT thành công</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau"><%=percent.ToString(MyUtility.MyConfig.DoubleFormat)%>%</label>
            </p>
            <p>
                <label>
                    Tổng MT:</label><label class="DanhDau"><%=Total.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng MT thành công:</label><label class="DanhDau"><%=TotalSuccess.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
        </fieldset>
    </div>
    <div runat="server" id="div_Charge" style="font-size: 12px;" visible="false">
        <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ Charge Tổng</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau"><%=Percent_Charge.ToString(MyUtility.MyConfig.DoubleFormat)%>%</label>
            </p>
            <p>
                <label>
                    Tổng Charge:</label><label class="DanhDau"><%=TotalCharge.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng Charge thành công:</label><label class="DanhDau"><%=TotalCharge_Success.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p><i>Chú ý: Tổng Charge là: Gia hạn +Đăng ký + Hủy đăng ký</i></p>
        </fieldset>
        <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ Charge Đăng ký</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau"><%=Percent_Charge_Reg.ToString(MyUtility.MyConfig.DoubleFormat)%>%</label>
            </p>
            <p>
                <label>
                    Tổng Charge:</label><label class="DanhDau"><%=TotalCharge_Reg.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng Charge thành công:</label><label class="DanhDau"><%=TotalCharge_Reg_Success.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p><i>Chú ý: Tổng Charge là: Đăng ký miễn phí + Đăng ký tính phí</i></p>
        </fieldset>
        <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ Charge Renew</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau"><%=Percent_Charge_Renew.ToString(MyUtility.MyConfig.DoubleFormat)%>%</label>
            </p>
            <p>
                <label>
                    Tổng Charge:</label><label class="DanhDau"><%=TotalCharge_Renew.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng Charge thành công:</label><label class="DanhDau"><%=TotalCharge_Renew_Success.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p><i>Chú ý: Tổng Charge là: Số lần gọi lệnh charge (1 thuê bao có thể gọi nhiều lần charge/1 ngày)</i></p>
        </fieldset>
         <fieldset style="width: 300px; margin-right: 30px;float: left; margin-top: 30px;">
            <legend>Tỷ lệ Charge Hủy (đồng bộ)</legend>
            <p>
                <label>
                    Tỷ lệ:</label><label class="DanhDau"><%=Percent_Charge_UnReg.ToString(MyUtility.MyConfig.DoubleFormat)%>%</label>
            </p>
            <p>
                <label>
                    Tổng Charge:</label><label class="DanhDau"><%=TotalCharge_UnReg.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p>
                <label>
                    Tổng Charge thành công:</label><label class="DanhDau"><%=TotalCharge_UnReg_Success.ToString(MyUtility.MyConfig.IntFormat)%></label>
            </p>
            <p><i>Chú ý: Tổng Charge là: Số lần đồng bộ Hủy sang Vinaphone</i></p>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
</asp:Content>
