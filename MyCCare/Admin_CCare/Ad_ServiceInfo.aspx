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
    <div class='p10 bor' style=" line-height:20px;">
        <h4 class='titlecheck'>Mô tả dịch vụ:</h4>
        <p>Dịch vụ Triệu phú Thể thao là dịch vụ cung cấp các thông tin tổng hợp (cập nhật kết quả trận đấu, sự kiện, bình luận, các thông tin bên lề.v.v) về các giải bóng đá trong nước và quốc tế đang diễn ra như Giải bóng đá Ngoại hạng Anh, Giải bóng đá Tây Ban Nha, Cúp C1 Châu Âu ... Dịch vụ triển khai từ ngày 25/08/2013 đến ngày 24/08/2015. Các khách hàng đăng ký dịch vụ hàng ngày sẽ nhận được bản tin tổng hợp về các giải đấu, trận đấu qua tin nhắn SMS và sẽ được tham gia MIẾN PHÍ chương trình khuyến mại đặc biệt “Sôi động cùng Triệu phú thể thao”: tham gia dự đoán về các trận đấu và quay số may mắn để có cơ hội nhận các giải thưởng hấp dẫn từ chương trình.</p>
        <br />
        <h4 class='titlecheck'>Cách sử dụng dịch vụ:</h4>
        <p>Để đăng ký dịch vụ và có cơ hội tham gia chương trình khuyến mại, khách hàng soạn tin DK gửi 9696 (cước thuê bao: 5.000đ/ngày, miễn phí ngày đầu tiên cho các khách hàng đăng ký dịch vụ lần đầu).</p>
        <div class="guide">
            <ul class="first" style="list-style: none;">
                <li><span style="font-size: 14px; font-weight: bold;">1. Giới thiệu chương trình khuyến mại:</span><br />
                    Là thuê bao dịch vụ Triệu phú Thể thao, khách hàng sẽ được hưởng các quyền lợi thuộc chương trình khuyến mại: “Sôi động cùng Triệu phú thể thao” như sau:
                        <ul style=" margin-left:25px;">
                            <li style="list-style-type: square;">Mỗi ngày, chương trình sẽ đưa ra 1 trận đấu thuộc 1 giải đấu trong nước hoặc quốc tế (Giải ngoại hạng Anh, giải bóng đá Tây Ban Nha, cúp C1) sẽ diễn ra trong ngày đó. Các khách hàng là thuê bao dịch vụ sẽ được mời tham gia dự đoán miễn phí về 5 nội dung khác nhau liên quan đến trận đấu bao gồm:
                                <ul style=" margin-left:25px;">
                                    <li style="list-style-type:circle;">Dự đoán kết quả trận đấu (thắng – hòa – thua):<br />
                                        Cú pháp dự đoán: KQ 1 hoặc KQ 2 hoặc KQ 3 trong đó KQ 1 tương ứng với đội đứng trước thắng, KQ 2 tương ứng với kết quả Hòa, KQ 3 tương ứng với đội đứng sau thắng. </li>
                                    <li style="list-style-type: circle;">Dự đoán tổng số bàn thắng của trận đấu:<br />
                                        Cú pháp dự đoán: BT X trong đó X tương ứng với tổng số bàn thắng cả 2 đội ghi được trong thời gian thi đấu chính thức của trận đấu. </li>
                                    <li style="list-style-type: circle;">Dự đoán tỉ số trong thời gian thi đấu chính thức của trận đấu:
                                        <br />
                                        Cú pháp dự đoán: TS X Y trong đó X tương ứng với số bàn thắng đội đứng trước ghi được, Y tương ứng với số bàn thắng đội đứng sau ghi được trong thời gian thi đấu chính thức của trận đấu. </li>
                                    <li style="list-style-type: circle;">Dự đoán đội giữ bóng nhiều hơn trong trận đấu<br />
                                        Cú pháp dự đoán: GB 1 hoặc GB 2 hoặc GB 3 trong đó GB 1 tương ứng với đội đứng trước giữ bóng nhiều hơn, GB 2 tương ứng với 2 đội có tỉ lệ giữ bóng như nhau, GB 3 tương ứng với đội đứng sau giữ bóng nhiều hơn. </li>
                                    <li style="list-style-type: circle;">Dự đoán tổng số thẻ vàng của trong thời gian thi đấu chính thức của trận đấu.<br />
                                        Cú pháp dự đoán: TV Z trong đó Z tương ứng với tổng số thẻ vàng trọng tài rút ra cho 2 đội trong thời gian thi đấu chính thức của trận đấu. </li>
                                </ul>
                            </li>
                            <li style="list-style-type: square;">Khách hàng có thể gửi nhiều tin nhắn dự đoán khác nhau về cùng 1 nội dung, tuy nhiên tổng số tin nhắn tối đa khách hàng được phép gửi cho 1 trận đấu cho cả 5 dự đoán là: 10 tin nhắn. Với mỗi dự đoán, hệ thống sẽ ghi nhận tin nhắn dự đoán cuối cùng của khách hàng là dự đoán chính thức để xem xét tính điểm cũng như tính thời gian tham gia của khách hàng.</li>
                            <li style="list-style-type: square;">Thời gian để khách hàng tham gia dự đoán cho 1 trận đấu sẽ bắt đầu từ 8h30’00” cho đến 23h59’59” cùng ngày hoặc cho đến hết hiệp 1 của trận đấu. Thời gian tính theo thời gian của hệ thống. </li>
                            <li style="list-style-type: square;">Tin nhắn dự đoán của ngày nào sẽ chỉ có giá trị cho trận đấu mà chương trình đưa ra trong ngày hôm đó.</li>
                        </ul>
                </li>
                <li><span style="font-size: 14px; font-weight: bold;">2. Giải thưởng:</span>
                     <ul style=" margin-left:25px;">
                        <li style="list-style: none;">Cơ cấu giải thưởng chương trình khuyến mại “Sôi động cùng Triệu phú thể thao, cơ hội trúng thưởng 10 triệu mỗi ngày”:
                               
                                   <div>
                                        <table cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <th>Cơ cấu giải thưởng
                                                    </th>
                                                    <th>Giá trị (đồng)
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>Giá trị giải đặc biệt tuần
                                                    </td>
                                                    <td>30.000.000
                                                    </td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                
                        </li>
                    </ul>
                </li>
                <li><span style="font-size: 14px; font-weight: bold;">3. Cách thức tính lũy điểm:</span><br />
                    Thuê bao được cộng các loại điểm như sau:
                        <ul style=" margin-left:25px;">
                            <li style="list-style-type: square;">Điểm khi đăng ký mới dịch vụ: Thuê bao đăng ký dịch vụ Triệu phú Thể thao thành công: 1.000 điểm/lần đăng ký. Áp dụng với các trường hợp: thuê bao đăng ký lần đầu được miễn cước, thuê bao đăng ký từ lần thứ hai và bị trừ cước, thuê bao đã hủy và đăng ký lại dịch vụ.</li>
                            <li style="list-style-type: square;">Điểm gia hạn thành công: tùy từng mức cước ngày sẽ có mức điểm cộng tương ứng
                               
                                     <table cellpadding="0" cellspacing="0" style="width: 300px;">
                                        <tbody>
                                            <tr>
                                                <th>STT
                                                </th>
                                                <th>Mức trừ cước
                                                </th>
                                                <th>Điểm tương ứng
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>1
                                                </td>
                                                <td>5.000đ/ngà
                                                </td>
                                                <td>1.000
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>2
                                                </td>
                                                <td>3.000đ/ngày
                                                </td>
                                                <td>600
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>3
                                                </td>
                                                <td>1.000đ/ngày
                                                </td>
                                                <td>200
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                
                            </li>
                            <li style="list-style-type: square;">Điểm trả lời câu hỏi của chương trình:
                              <ul style=" margin-left:25px;">
                                    <li   style="list-style-type:circle;">Dự đoán đúng: 500 điểm/lần dự đoán đúng.</li>
                                    <li  style="list-style-type:circle;">Dự đoán sai: 0 điểm/lần dự đoán sai. </li>
                                </ul>
                            </li>
                        </ul>
                </li>
                <li><span style="font-size: 14px; font-weight: bold;">4. Cách thức xác định thuê bao trúng thưởng:</span>
                     <ul style=" margin-left:25px;">
                        <li style="list-style-type: square;">Tổng số điểm tuần của thuê bao là tổng số điểm mà thuê bao tích lũy được trong một chu kỳ tuần, tính từ 0h00:00 thứ 2 đến 23h59:59 Chủ nhật hàng tuần.</li>
                        <li style="list-style-type: square;">Tiêu chí để xét trao thưởng giải tuần theo thứ tự ưu tiên như sau: 
                                <ul style=" margin-left:25px;">
                                    <li style="list-style-type:circle;">Ưu tiên 1: Tổng số điểm tích lũy cao nhất và số lần dự đoán đúng nhiều nhất.</li>
                                    <li style="list-style-type:circle;">Ưu tiên 2: Nếu có nhiều hơn 1 thuê bao thỏa mãn đồng thời các điều kiện ưu tiên 1, thuê bao trúng thưởng là thuê bao có tổng thời gian dự đoán theo chu kỳ tuần ít nhất. Tổng thời gian dự đoán theo chu kỳ ngày là tổng thời gian tính từ thời điểm khách hàng nhận được câu hỏi dự đoán đầu tiên của ngày đến thời điểm khách hàng dừng dự đoán của ngày đó. Tổng thời gian dự đoán theo chu kỳ tuần là tích lũy tổng thời gian dự đoán ngày tính từ thứ 2 đến Chủ nhật hàng tuần.</li>
                                    <li style="list-style-type:circle;">Ưu tiên 3: Nếu có nhiều hơn 1 thuê bao thỏa mãn đồng thời các điều kiện ưu tiên 1 và 2, thuê bao trúng thưởng là thuê bao có thời gian bắt đầu dự đoán sớm nhất.</li>
                                </ul>
                        </li>
                        <li style="list-style-type: square;">Một thuê bao có thể được trúng nhiều giải thưởng tuần.</li>
                    </ul>
                </li>
                <li><span style="font-size: 14px; font-weight: bold;">5. Thời gian xác định thuê bao trúng thưởng:</span>
                    <ul style=" margin-left:25px;">
                        <li style="list-style-type: square;">Thời gian xác định thuê bao trúng thưởng được thực hiện vào thứ hai hàng tuần và chọn ra thuê bao trúng thưởng giải tuần của tuần trước đó.</li>
                    </ul>
                </li>
            </ul>
        </div>
        <br />
        <h4 class='titlecheck'>Giá cước:</h4>
        <p>5.000/Ngày</p>
        <br />
        <h4 class='titlecheck'>Đăng ký/Hủy:</h4>
        <p>Qua SMS: DK gửi 9696/ HUY gửi 9696</p>
    </div>
</asp:Content>
