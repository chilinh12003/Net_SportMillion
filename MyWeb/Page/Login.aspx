<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyWeb.Page.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <a class="home" href="http://thethao.vinaphone.com.vn"></a><a class="item active" href="http://thethao.vinaphone.com.vn/Page/ListCode.aspx">Kiểm tra điểm</a> <a class="item " href="http://thethao.vinaphone.com.vn/Page/Guide.htm">Hướng dẫn cách chơi</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/Result.htm">Khách hàng trúng thưởng</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/Rule.htm">Thể lệ giải thưởng</a> <a class="item" href="http://thethao.vinaphone.com.vn/Page/FAQ.htm">Hỏi đáp</a> <a class="item" href="#">Liên hệ</a>
        </div>
    </div>
    <div class="content content-none">
        <div class="center center-relative guide-with">
            <div class="transparent">
            </div>
            <div class="guide">
                <center class="mdt">
                    <h2>
                        TRA CỨU MÃ DỰ THƯỞNG</h2>
                    <div class="line">
                        <table class="login">
                            <tbody>
                                <tr>
                                    <td>
                                        <label class="title">
                                            Số điện thoại:</label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" id="tbx_MSISDN" onkeypress="return isNumberKey_int(event);"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="title">
                                            Mật khẩu:</label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" id="tbx_Password" onkeypress="return isNumberKey_int(event);"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <i>Để lấy mật khẩu xin vui lòng soan tin <b>MK</b> gửi đến <b>9696</b> (miễn phí gửi tin nhắn)</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btn_Login" Text="Tra cứu" OnClick="btn_Login_Click" />
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
