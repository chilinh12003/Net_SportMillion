<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_News_Edit.aspx.cs" Inherits="MyAdmin.Admin_News.Ad_News_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
     <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet"
        type="text/css" />

    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href="Ad_News.aspx" runat="server" id="link_Cancel"><span class="Cancel"></span>Hủy </a>
    <asp:LinkButton runat="server" ID="lbtn_Save" OnClick="lbtn_Save_Click" OnClientClick="return CheckAll();">
     <span class="Save"></span>
            Lưu
    </asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbtn_Accept" OnClick="lbtn_Apply_Click" OnClientClick="return CheckAll();">
     <span class="Accept"></span>
            Apply
    </asp:LinkButton>
    <a href="Ad_News_Edit.aspx" runat="server" id="link_Add"><span class="Add"></span>Thêm </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <div class="Edit_Left">       
        <div class="Edit_Title" style="height: 40px;">
            Thời gian trả tin</div>
        <div class="Edit_Control" style="height: 50px;">
         <div class="NewLine" style="padding-bottom:3px; padding-top:0px;">
            <label>Chọn thời gian trả tin cho dịch vụ:</label><label runat="server" id="label_PushTime"></label>
            </div>
            <input type="text" runat="server" id="tbx_PushTime" style="width: 70px;" />
            <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_PushTime.ClientID %>'),'dd/mm/yyyy',this)" />
            <div>
                <label>
                    Giờ:</label></div>
            <select runat="server" id="sel_PushHour">
            </select>
            <div>
                <label>
                    Phút:</label></div>
            <select runat="server" id="sel_PushMinute">
            </select>
            <div style="display:none;">
                <label>
                    Giây:</label></div>
            <select  style="display:none;" runat="server" id="sel_PushSecond">
            </select>
           
        </div>       
        <div class="Edit_Title">
            Tiêu đề
        </div>       
         <div class="Edit_Control">
         <input type="text" runat="server" id="tbx_NewsName" style="width: 99%;" />
         </div>
        <div class="NewLine">
            <div class="Edit_Title" style="height: 150px;">
                Nội dung:</div>
            <div class="Edit_Control_Editor">
                <textarea id="tbx_Contents" onkeyup="return CheckMaxLength(this,479,event);" runat="server" style="float: left; height: 150px; width: 99%;"></textarea>
                <div id="div_Length" style="float: left; margin-top: 6px; width: 100%; font-size: 12px; font-weight: bold;">
                    Bạn đã nhập vào 0/800 ký tự</div>
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
            <div class="Properties_Title" >
                Tình trạng:</div>
            <div class="Properties_Control">
                <select runat="server" id="sel_Status">
                </select>
            </div>
            <div class="Properties_Title">
                Loại tin:</div>
            <div class="Properties_Control">
                <select runat="server" id="sel_NewsType">
                </select>
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
