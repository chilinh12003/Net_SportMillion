<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_ServiceInfo.aspx.cs" Inherits="MyCCare.Admin_CCare.Ad_ServiceInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Content" runat="server">
     <div id="menutabs1" class='mt10'>
        <a class="" href="Ad_SubInfo.aspx">
            <img class='icon1' src='../images/icon1.png'><span>Tra cứu thuê bao</span></a>
        <a class="" href="Ad_HistoryRegDereg.aspx">
            <img class='icon2' src='../images/icon2.png'><span>Tra cứu sử dụng dịch vụ</span></a>
        <a class="" href="Ad_RegDereg.aspx">
            <img class='icon3' src='../images/icon3.png'><span>Cài đặt dịch vụ</span></a>
        <a class="selected" href="Ad_ServiceInfo.aspx">
            <img class='icon4' src='../images/icon4.png'><span>Thông tin dịch vụ</span></a>
    </div>   
    <div class='p10 bor'>
        <h4 class='titlecheck'>Mô tả dịch vụ:</h4>
        <p>Dịch vụ Triệu phú Thể thao là dịch vụ cung cấp các thông tin tổng hợp (cập nhật kết quả trận đấu, sự kiện, bình luận, các thông tin bên lề.v.v) về các giải bóng đá trong nước và quốc tế đang diễn ra như Giải bóng đá Ngoại hạng Anh, Giải bóng đá Tây Ban Nha, Cúp C1 Châu Âu ... Dịch vụ triển khai từ ngày 25/08/2013 đến ngày 24/08/2015. Các khách hàng đăng ký dịch vụ hàng ngày sẽ nhận được bản tin tổng hợp về các giải đấu, trận đấu qua tin nhắn SMS và sẽ được tham gia MIẾN PHÍ chương trình khuyến mại đặc biệt “Sôi động cùng Triệu phú thể thao”: tham gia dự đoán về các trận đấu và quay số may mắn để có cơ hội nhận các giải thưởng hấp dẫn từ chương trình.</p>
        <br />
        <h4 class='titlecheck'>Cách sử dụng dịch vụ:</h4>
        <p>Để đăng ký dịch vụ và có cơ hội tham gia chương trình khuyến mại, khách hàng soạn tin DK gửi 9696 (cước thuê bao: 5.000đ/ngày, miễn phí ngày đầu tiên cho các khách hàng đăng ký dịch vụ lần đầu).</p>
        <br />
        <h4 class='titlecheck'>Giá cước:</h4>
        <p>5.000/Ngày</p>
        <br />
        <h4 class='titlecheck'>Đăng ký/Hủy:</h4>
        <p>Qua SMS: DK gửi 9696/ HUY gửi 9696</p>
    </div>
</asp:Content>
