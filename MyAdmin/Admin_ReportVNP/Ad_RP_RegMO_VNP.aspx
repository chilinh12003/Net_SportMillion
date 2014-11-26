<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_RP_RegMO_VNP.aspx.cs" Inherits="MyAdmin.Admin_ReportVNP.Ad_RP_RegMO_VNP" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
    <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href="<%=LinkExportExcel() %>"><span class="Export"></span>Export</a>
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
    <table class="Data" border="0" cellpadding="4" cellspacing="0">
        <tbody>
            <tr class="Table_Header">
                <th class="Table_TL border-none"></th>
                <th class="last " rowspan="2">STT</th>
                <th class="last" rowspan="2">Ngày</th>
                 <th colspan="2">MT</th>
                <th colspan="6">MO</th>
                <th colspan="6">MO Đăng ký</th>
                <th colspan="6">MO Hủy</th>
                <th colspan="8">MO Dự đoán</th>
                <th class="Table_TR border-none"></th>
            </tr>
            <tr class="Table_Header">
                <th class="Table_TL_Repeat last border-none"></th>
                <th class="last">Tổng</th>
                <th class="last">Thất bại</th>
                <th class="last">Tổng</th>
                <th class="last">Thành công</th>
                <th class="last">Sai C.Pháp</th>
                <th class="last">Lỗi H.Thống</th>
                <th class="last">Lỗi khác</th>
                <th class="last">Tỷ lệ (%)</th>
                <th class="last">Tổng</th>
                <th class="last">Thành công</th>
                <th class="last">Không đủ tiền</th>
                <th class="last">Lỗi H.Thống</th>
                <th class="last">Lỗi khác</th>
                <th class="last">Tỷ lệ (%)</th>
                <th class="last">Tổng</th>
                <th class="last">Xác nhận hủy</th>
                <th class="last">Thành công</th>
                <th class="last">Thất bại</th>
                <th class="last">Lỗi H.Thống</th>
                <th class="last">Tỷ lệ (%)</th>
                <th class="last">Tổng</th>
                <th class="last">Thành công</th>
                <th class="last">Sai C.Pháp</th>
                <th class="last">Vượt MO/Ngày</th>
                <th class="last" title="Dự đoán khi thời gian dự đoán đã hết">Hết hạn</th>
                <th class="last">Lỗi H.Thống</th>
                <th class="last" title="Trứ tiền không thành công, Chưa DK đã dữ đoán, Lỗi khác" >Lỗi khác</th>
                <th class="last">Tỷ lệ (%)</th>
                <th class="Table_TR_Repeat last border-none"></th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr class="Table_Row_1">
                        <td class="Table_ML_1 border-none"></td>
                        <td><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                        <td><%#((DateTime)Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat) %></td>
                        
                        <td><%#((double)Eval("MTTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MTFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOInvalid")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MOSuccess")/(double)Eval("MOTotal")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>

                        <td><%#((double)Eval("MORegTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegBlanceTooLow")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MORegSuccess")/(double)Eval("MORegTotal")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>

                        <td><%#((double)Eval("MODeregTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregConfirm")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MODeregSuccess")/(double)Eval("MODeregConfirm")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        
                        <td><%#((double)Eval("MOAnswerTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerInvalid")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerOver")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerExpire")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MOAnswerSuccess")/(double)Eval("MOAnswerTotal")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td class="Table_MR_1 border-none"></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Table_Row_2">
                        <td class="Table_ML_2 border-none"></td>
                        <td><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                        <td><%#((DateTime)Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat) %></td>
                        
                        <td><%#((double)Eval("MTTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MTFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOInvalid")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MOSuccess")/(double)Eval("MOTotal")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>

                        <td><%#((double)Eval("MORegTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegBlanceTooLow")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MORegFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MORegSuccess")/(double)Eval("MORegTotal")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>

                        <td><%#((double)Eval("MODeregTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregConfirm")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MODeregError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MODeregSuccess")/(double)Eval("MODeregConfirm")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        
                        <td><%#((double)Eval("MOAnswerTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerSuccess")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerInvalid")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerOver")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerExpire")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerError")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#((double)Eval("MOAnswerFail")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td><%#(((double)Eval("MOAnswerSuccess")/(double)Eval("MOAnswerTotal")) *100 ).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
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
                <asp:Title Text="Biểu đồ: MO Đăng ký/ MO Hủy"></asp:Title>
            </Titles>
            <Series>
                <asp:Series ChartArea="ChartArea" Name="Series_Reg" LegendText="MO đăng ký" Legend="Legend1" ChartType="Column" BorderWidth="2" BorderColor="#418CF0" LabelForeColor="#418CF0">
                </asp:Series>
                <asp:Series ChartArea="ChartArea" Name="Series_Dereg" LegendText="MO Hủy" Legend="Legend1" ChartType="Column" BorderWidth="2" LabelForeColor="#FCB441"></asp:Series>
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
