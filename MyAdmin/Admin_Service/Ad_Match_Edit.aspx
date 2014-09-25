<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_Match_Edit.aspx.cs" Inherits="MyAdmin.Admin_Service.Ad_Match_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
    <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href="Ad_Match.aspx" runat="server" id="link_Cancel"><span class="Cancel"></span>Hủy </a>
    <asp:LinkButton runat="server" ID="lbtn_Save" OnClick="lbtn_Save_Click" OnClientClick="return CheckAll();">
     <span class="Save"></span>
            Lưu
    </asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbtn_Accept" OnClick="lbtn_Apply_Click" OnClientClick="return CheckAll();">
     <span class="Accept"></span>
            Apply
    </asp:LinkButton>
    <a href="Ad_Match_Edit.aspx" runat="server" id="link_Add"><span class="Add"></span>Thêm </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <div class="Edit_Left">
        <div class="Edit_Title">
            Trận đấu</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_TeamName1" />
            <span> - </span>
            <input type="text" runat="server" id="tbx_TeamName2" />
        </div>
        <div class="Edit_Title">
            Ngày quay thưởng</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_CodeDate" style="width: 70px;" />
            <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_CodeDate.ClientID %>'),'dd/mm/yyyy',this)" />
        </div>
        <div class="Edit_Title">
            Diễn ra lúc</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_PlayDate" style="width: 70px;" />
            <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_PlayDate.ClientID %>'),'dd/mm/yyyy',this)" />
            <label>
                Giờ:</label><select runat="server" id="sel_PlayHour"></select>
            <label>
                Phút:</label><select runat="server" id="sel_PlayMinute"></select>
        </div>
        <div class="Edit_Title">
            Bắt đầu dự đoán</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_BeginDate" style="width: 70px;" />
            <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_BeginDate.ClientID %>'),'dd/mm/yyyy',this)" />
            <label>
                Giờ:</label><select runat="server" id="sel_BeginHour"></select>
            <label>
                Phút:</label><select runat="server" id="sel_BeginMinute"></select>
        </div>
        <div class="Edit_Title">
            Kết thúc dự đoán</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_EndDate" style="width: 70px;" />
            <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_EndDate.ClientID %>'),'dd/mm/yyyy',this)" />
            <label>
                Giờ:</label><select runat="server" id="sel_EndHour"></select>
            <label>
                Phút:</label><select runat="server" id="sel_EndMinute"></select>
        </div>
        <div class="NewLine">
            <div class="Edit_Title" style="height: 50px;">
                Mổ tả thêm:</div>
            <div class="Edit_Control_Editor">
                <textarea id="tbx_Description" runat="server" style="float: left; height: 50px; width: 99%;"></textarea>
            </div>
        </div>
    </div>
    <div class="Edit_Right">
        <div class="Properties_Header">
            <div class="Properties_Header_In">
                Thông tin chi tiết khác
            </div>
        </div>
        <div class="Properties">
            <div class="Properties_Title">
                Tình trạng:</div>
            <div class="Properties_Control">
                <select runat="server" id="sel_Status">
                </select>
            </div>
            <div class="Properties_Title">
                Kết quả:</div>
            <div class="Properties_Control">
                <input type="text" runat="server" id="tbx_AnswerKQ" />
            </div>
            <div class="Properties_Title">
                Bàn thắng:</div>
            <div class="Properties_Control">
                <input type="text" runat="server" id="tbx_AnswerBT" /></div>
            <div class="Properties_Title">
                Giữ bóng:</div>
            <div class="Properties_Control">
                <input type="text" runat="server" id="tbx_AnswerGB" /></div>
            <div class="Properties_Title">
                Tỷ số:</div>
            <div class="Properties_Control">
                <input type="text" runat="server" id="tbx_AnswerTS" /></div>
            <div class="Properties_Title">
                Thẻ vàng:</div>
            <div class="Properties_Control">
                <input type="text" runat="server" id="tbx_AnswerTV" />
            </div>
            <div class="Properties_Title">
                Ưu tiên:</div>
            <div class="Properties_Control">
                <input type="text" runat="server" id="tbx_Priority" value="0" onkeypress="return isNumberKey_int(event);" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
    <script language="javascript" type="text/javascript">
            

    </script>
</asp:Content>
