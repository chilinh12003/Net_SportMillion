﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCode.aspx.cs" Inherits="MyWeb.Page.ListCode1" %>

<%@ Register Src="Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Triệu phú thể thao</title>
    <link href="../Style/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Forall.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="banner">
        <div class="center center-relative">
            <a class="logo" href="http://vinaphone.com.vn"></a>
            <div class="menu-sub">
                <div class="repeat">
                    <a class="item" href="http://vinaphone.com.vn/supports/dangkydichvu">Đăng ký DV</a><span class="item split">|</span> <a class="item" href="http://billing.vinaphone.com.vn/">Tra cước</a><span class="item split">|</span> <a class="item" href="http://chonso.vinaphone.com.vn/">Chọn số</a><span class="item split">|</span> <a class="item" href="http://cskh.vinaphone.com.vn/">CSKH</a><span class="item split">|</span> <a class="item" href="http://vinaphone.com.vn/personals/homepage">Trang cá nhân</a> <a class="item-login margin-left" href="http://vinaphone.com.vn/login.jsp?lang=vi">Đăng nhập</a><span class="item-login split">|</span> <a class="item-login" href="http://vinaphone.com.vn/personal/register.do">Đăng ký</a></div>
                <div class="left">
                </div>
            </div>
        </div>
    </div>
    <div class="menu">
        <div class="center">
            <a class="home" href="http://thethao.vinaphone.com.vn"></a><a class="item active" href="http://thethao.vinaphone.com.vn/Page/ListCode.aspx">Kiểm tra điểm</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/Guide.htm">Hướng dẫn cách chơi</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/Result.htm">Khách hàng trúng thưởng</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/Rule.htm">Thể lệ giải thưởng</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/FAQ.htm">Hỏi đáp</a> <a class="item" href="#">Liên hệ</a>
        </div>
    </div>
    <div class="content content-none">
        <div class="center center-relative guide-with">
            <div class="transparent">
            </div>
            <div class="guide">
                <center class="list-mdt">
                    <h2>
                        DANH SÁCH ĐIỂM TRONG THÁNG CHO SỐ ĐIỆN THOẠI <%=MySetting.WebSetting.GetMSISDN() %></h2>
                   
                    <div class="line" style="padding: 10px;">
                        <table style="width: 500px;">
                            <tbody>
                                <tr>
                                    <th>
                                        STT
                                    </th>
                                    <th>
                                        Tổng điểm trong ngày
                                    </th>
                                    <th>
                                        Ngày
                                    </th>
                                </tr>
                                <asp:Repeater runat="server" ID="rpt_Data">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#(Container.ItemIndex + PageIndex).ToString()%>
                                            </td>
                                            <td>
                                                <%#Eval("TotalMark")%>
                                            </td>
                                            <td>
                                                <%#Eval("CodeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("CodeDate")).ToString(MyUtility.MyConfig.ShortDateFormat)%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="3">
                                        <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3">
                                        <label>Bạn muốn tra cưu với số điện thoại khác, xin hãy click <asp:LinkButton runat="server" ID="lbtn_LogOut" OnClick="lbtn_LogOut_Click" CssClass="logout"> VÀO ĐÂY </asp:LinkButton> </label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </center>
            </div>
        </div>
    </div>
    <div class="footer">
        <div class="center center-relative">
            <a class="logo" href="http://vinaphone.com.vn"></a>
            <div class="footer-menu">
                <a class="item" href="http://www.vinaphone.com.vn/">Trang chủ</a><span class="item split">|</span> <a class="item" href="http://www.vinaphone.com.vn/aboutus/gioithieu">Giới thiệu</a><span class="item split">|</span> <a class="item" href="http://www.vinaphone.com.vn/new/homepage">Tin tức</a><span class="item split">|</span> <a class="item" href="http://www.vinaphone.com.vn/services/homepage">Dịch vụ</a><span class="item split">|</span> <a class="item" href="http://vinaphone.com.vn/devices/homepage">Thiết bị</a><span class="item split">|</span> <a class="item" href="http://www.vinaphone.com.vn/products/homepage">Gói cước</a><span class="item split">|</span> <a class="item" href="http://vinaphone.com.vn/supports/homepage">Hỗ trợ</a><span class="item split">|</span> <a class="item" href="http://vinaphone.com.vn/supports/support">Liên hệ</a><span class="item split">|</span> <a class="item" href="http://vinaphone.com.vn/locale.do?language=en">English</a>
                <div class="info-pad info">
                    Phát triển bởi VinaPhone 2005-2013. Bản quyền đã đăng ký.</div>
                <div class="info">
                    Giấy phép số: 337/GP-BC do Bộ Thông tin - Truyền thông cấp ngày 10/11/2006.
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
