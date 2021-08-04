<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCoverImage.ascx.cs" Inherits="WebApplication1.ucCoverImage" %>
<div runat="server" id="divMain" style="background-color:aqua" >
    <img src="IMG_3756.jpg" id="imgCover" runat="server" alt="Alternate Text" width="80" />
    <span>
        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
    </span>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
</div>
