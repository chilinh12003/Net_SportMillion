﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_HistoryMark.aspx.cs" Inherits="MyCCare.Admin_CCare.Ad_HistoryMark" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Content" runat="server">
    <div id="menutabs1" class='mt10'>
        <a class="" href="Ad_SubInfo.aspx">
            <img class='icon1' src='../images/icon1.png'><span>Tra cứu thuê bao</span></a>
        <a class="selected" href="Ad_HistoryRegDereg.aspx">
            <img class='icon2' src='../images/icon2.png'><span>Tra cứu sử dụng dịch vụ</span></a>
        <a class="" href="Ad_RegDereg.aspx">
            <img class='icon3' src='../images/icon3.png'><span>Cài đặt dịch vụ</span></a>
        <a class="" href="Ad_ServiceInfo.aspx">
            <img class='icon4' src='../images/icon4.png'><span>Thông tin dịch vụ</span></a>
    </div>
    <div class='tabtracuusddv'>
        <ul>
            <li><a href='Ad_HistoryRegDereg.aspx'>Lịch sử Đăng ký / Hủy</a></li>
            <li><a href='Ad_HistoryCharge.aspx'>Lịch sử trừ cước</a></li>
            <li><a href='Ad_HistoryUsing.aspx'>Lịch sử sử dụng</a></li>
            <li><a href='Ad_HistoryMOMT.aspx' class='select'>Lịch sử MO /MT</a></li>
        </ul>
    </div>
    <div class='fillterarea'>
        <table>
            <tr>
                <td width='100'>Số thuê bao:</td>
                <td colspan="5">
                    <input style='width: 147px' type='text' class='textbox' runat="server" id="tbx_MSISDN" /></td>
                <td rowspan='2' align='right' width='200'>
                    <asp:Button runat="server" CssClass="btn_search" ID="btn_Search" Text="Tra cứu" OnClick="btn_Search_Click" /></td>
            </tr>
            <tr>
                <td>Gói cước:</td>
                <td>
                    <select style='width: 150px' class="dropdownlist" runat="server" id="sel_Service">
                        <option>-- Chọn gói cước --</option>
                    </select></td>
                <td>Thời gian bắt đầu:</td>
                <td>
                    <input type="text" class="datetime" runat="server" id="tbx_FromDate" onclick="displayCalendar(this, 'dd/mm/yyyy', this);" /></td>

                <td>Thời gian kết thúc</td>
                <td>
                    <input type="text" class="datetime" runat="server" id="tbx_ToDate" onclick="displayCalendar(this, 'dd/mm/yyyy', this);" /></td>
            </tr>
        </table>
    </div>
    <table class="tbl_style">
        <tbody>
            <tr>
                <td width='20%' valign='top'>
                    <div class='menuleftmdt'>
                        <ul>
                            <li><a href='#' class='active'>Điểm tuần</a></li>
                            <li><a href='GUI - Tra cứu sử dụng dịch vụ - Tra cứu mã dự thưởng - Tuan.html'>Mã dự thưởng tuần</a></li>
                            <li><a href='GUI - Tra cứu sử dụng dịch vụ - Tra cứu mã dự thưởng - Ngay.html'>Mã dự thưởng ngày</a></li>
                            <li><a href='GUI - Tra cứu sử dụng dịch vụ - Tra cứu mã dự thưởng - Lspsmdt.html'>Lịch sử phát sinh mã dự thưởng</a></li>
                        </ul>
                    </div>
                </td>
                <td>
                    <div class='p8'>
                        <h4 class='mb10'>Tổng mã dự thưởng tháng:</h4>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>