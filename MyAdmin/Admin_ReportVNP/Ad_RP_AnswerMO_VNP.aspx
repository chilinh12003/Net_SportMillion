<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_RP_AnswerMO_VNP.aspx.cs" Inherits="MyAdmin.Admin_ReportVNP.Ad_RP_AnswerMO_VNP" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
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
    <asp:Button runat="server" ID="btn_Search" Text="Tìm kiếm" OnClick="btn_Search_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
    <table class="Data" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr class="Table_Header">
                <th class="Table_TL border-none"></th>
                <th class="last " rowspan="2">STT</th>
                <th class="last" rowspan="2">Ngày</th>
                <th colspan="7">MO Dự đoán</th>
                <th class="Table_TR border-none"></th>
            </tr>
            <tr class="Table_Header">
                <th class="Table_TL_Repeat last border-none"></th>
                <th class="last">Tổng</th>
                <th class="last">Thành công</th>
                <th class="last">Sai cú pháp</th>
                <th class="last">Vượt SL MO/Ngày</th>
                <th class="last">Hết hạn Dự đoán</th>
                <th class="last">Lỗi hệ thống</th>
                <th class="last" title="Trứ tiền không thành công, Chưa DK đã dữ đoán, Lỗi khác" >Lỗi khác</th>
              
                <th class="Table_TR_Repeat last border-none"></th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr class="Table_Row_1">
                        <td class="Table_ML_1 border-none"></td>
                        <td><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                        <td><%#((DateTime)Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat) %></td>
                        <td><%#((double)Eval("MOAnswerTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerInvalid")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerOver")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerExpire")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td class="Table_MR_1 border-none"></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Table_Row_2">
                        <td class="Table_ML_2 border-none"></td>
                        <td><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                        <td><%#((DateTime)Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat) %></td>
                        <td><%#((double)Eval("MOAnswerTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerInvalid")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerOver")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerExpire")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td class="Table_MR_2 border-none"></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            </tbody>
    </table>
    <div class="Table_Footer">
        <div class="Table_BL">
            <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
        </div>
        <div class="Table_BR">
        </div>
    </div>
    <div style="width:100%; text-align:center; position:relative; overflow:auto; height:500px; padding-top:15px;">
        <asp:Chart ID="chart_Reg" runat="server" Height="450" Width="890" CssClass="chart;" BorderlineDashStyle="Solid" BorderlineColor="Silver" BackColor="">
             <Titles>
                <asp:Title Text="Biểu đồ: Tỷ lệ MO dự đoán"></asp:Title>
            </Titles>
            <Series>
                <asp:Series ChartArea="ChartArea" Name="Series_Total" LegendText="Tổng" Legend="Legend1" ChartType="Column" BorderWidth="2" BorderColor="#418CF0" LabelForeColor="#418CF0">
                </asp:Series>
                <asp:Series ChartArea="ChartArea" Name="Series_Success" LegendText="Thành công" Legend="Legend1" ChartType="Column" BorderWidth="2" LabelForeColor="#FCB441"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea" ></asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Alignment="Near" BackImageAlignment="Center" Docking="Top">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
</asp:Content>
