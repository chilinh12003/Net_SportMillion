<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ad_SubInfo.aspx.cs" Inherits="MyAdmin.Admin_CCare.Ad_SubInfo" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/Admin.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Admin_Paging.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ForAll.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/SubInfo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="page-name border-all">
            THÔNG TIN KHÁCH HÀNG SỬ DỤNG DỊCH VỤ TRIỆU PHÚ THỂ THAO TRONG THÁNG
            <%=DateTime.Now.ToString("MM-yyyy") %>
        </div>
        <div class="left border-all">
            <div class="subtitle border-bottom">
                Dịch vụ đã đăng ký</div>
            <div class="content-left">
                <asp:Repeater runat="server" ID="rpt_Sub">
                    <ItemTemplate>
                        <p class="line">
                            <label class="name">
                                Dịch vụ:
                            </label>
                            <label class="value">
                                Triệu phú thể thao</label></p>
                        <p class="line">
                            <label class="name">
                                Ngày đăng ký:
                            </label>
                            <label class="value">
                                <%#Eval("EffectiveDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("EffectiveDate")).ToString(MyUtility.MyConfig.LongDateFormat)%></label></p>
                        <p class="line">
                            <label class="name">
                                Ngày hết hạn:
                            </label>
                            <label class="value">
                                <%#Eval("ExpiryDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ExpiryDate")).ToString(MyUtility.MyConfig.LongDateFormat)%></label></p>
                                  <p class="line">
                            <label class="name">
                                Kênh đăng ký:
                            </label>
                            <label class="value">
                                <%#Eval("ChannelTypeName")%></label></p>
                        <p class="line">
                            <label class="name">
                                Điểm trong ngày:
                            </label>
                            <label class="value">
                                <%#Eval("MarkByDay")%></label></p>
                        <p class="line">
                            <label class="name">
                                Mã dự thưởng trong ngày:
                            </label>
                            <label class="value">
                                <%#Eval("CodeByDay")%></label></p>
                        <p class="line">
                            <label class="name">
                                Dự đoán Kết quả:
                            </label>
                            <label class="value">
                                <%# string.IsNullOrEmpty(Eval("AnswerKQ").ToString()) ? "Chưa trả lời" : Eval("AnswerKQ")%></label></p>
                        <p class="line">
                            <label class="name">
                                Dự đoán Bàn thắng:
                            </label>
                            <label class="value">
                                <%# string.IsNullOrEmpty(Eval("AnswerBT").ToString()) ? "Chưa trả lời" : Eval("AnswerBT")%></label></p>
                        <p class="line">
                            <label class="name">
                                Dự đoán Tỷ số:
                            </label>
                            <label class="value">
                                <%# string.IsNullOrEmpty(Eval("AnswerTS").ToString()) ? "Chưa trả lời" : Eval("AnswerTS")%></label></p>
                        <p class="line">
                            <label class="name">
                                Dự đoán Giữ bóng:
                            </label>
                            <label class="value">
                                <%# string.IsNullOrEmpty(Eval("AnswerGB").ToString()) ? "Chưa trả lời" : Eval("AnswerGB")%></label></p>
                        <p class="line">
                            <label class="name">
                                Dự đoán Thẻ vàng:
                            </label>
                            <label class="value">
                                <%# string.IsNullOrEmpty(Eval("AnswerTV").ToString()) ? "Chưa trả lời" : Eval("AnswerTV")%></label></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="right border-all">
            <div class="subtitle border-bottom">
                <asp:Button runat="server" ID="btn_MOlog" OnClick="btn_MOlog_Click" Text="Lịch sử Đăng ký/Hủy/Trả tin" />
                <asp:Button runat="server" ID="btn_ChargeLog" OnClick="btn_ChargeLog_Click" Text="Lịch sử trừ tiền" />
            </div>
            <div class="NoiDung">
                <div runat="server" id="div_2">
                    <table class="Data" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr class="Table_Header">
                                <th class="Table_TL">
                                </th>
                                <th width="10">
                                    STT
                                </th>
                                <th>
                                    Số điện thoại
                                </th>
                                <th>
                                    MO
                                </th>
                                <th>
                                    Kênh
                                </th>
                                <th>
                                    Trường hợp
                                </th>
                                <th style="width: 40%;">
                                    MT
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_2" CommandArgument="LogDate DESC" OnClick="lbtn_Sort_Click">Ngày trả tin</asp:LinkButton>
                                </th>
                                <th class="Table_TR">
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt_Data_2">
                                <ItemTemplate>
                                    <tr class="Table_Row_1">
                                        <td class="Table_ML_1">
                                        </td>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("MSISDN")%>
                                        </td>
                                        <td>
                                            <%#Eval("MO")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChannelTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MTTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MT")%>
                                        </td>
                                        <td>
                                            <%#Eval("LogDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("LogDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td class="Table_MR_1">
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="Table_Row_2">
                                        <td class="Table_ML_2">
                                        </td>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("MSISDN")%>
                                        </td>
                                        <td>
                                            <%#Eval("MO")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChannelTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MTTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MT")%>
                                        </td>
                                        <td>
                                            <%#Eval("LogDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("LogDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td class="Table_MR_2">
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div runat="server" id="div_3">
                    <table class="Data" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr class="Table_Header">
                                <th class="Table_TL">
                                </th>
                                <th width="10">
                                    STT
                                </th>
                                <th>
                                    Số điện thoại
                                </th>
                                <th>
                                    Giá
                                </th>
                                <th>
                                    Hình thức
                                </th>
                                <th>
                                    Tình trạng
                                </th>
                                <th>
                                    Kênh
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_3" CommandArgument="ChargeDate DESC" OnClick="lbtn_Sort_Click">Thời gian</asp:LinkButton>
                                </th>
                                <th class="Table_TR">
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt_Data_3">
                                <ItemTemplate>
                                    <tr class="Table_Row_1">
                                        <td class="Table_ML_1">
                                        </td>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("MSISDN")%>
                                        </td>
                                        <td>
                                            <%#Eval("Price")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeStatusName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChannelTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td class="Table_MR_1">
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="Table_Row_2">
                                        <td class="Table_ML_2">
                                        </td>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("MSISDN")%>
                                        </td>
                                        <td>
                                            <%#Eval("Price")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeStatusName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChannelTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td class="Table_MR_2">
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="Table_Footer">
                    <div class="Table_BL">
                        <uc1:Admin_Paging ID="Admin_Paging2" runat="server" />
                        <uc1:Admin_Paging ID="Admin_Paging3" runat="server" />
                    </div>
                    <div class="Table_BR">
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
