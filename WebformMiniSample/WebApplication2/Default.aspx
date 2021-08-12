﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        #txt1 {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label runat="server" ID="lblName">Hello</asp:Label><br />

        <asp:TextBox runat="server" ID="txt1"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hf1" />
        <asp:HiddenField runat="server" ID="hf2" />



        <%-- 瀏覽器端的警告訊息 --%>
        <asp:Button ID="Unnamed" runat="server" Text="Button" OnClick="Unnamed_Click" OnClientClick="exec(); alert('123')" />

        <script>
            // 瀏覽器端的警告訊息
            //alert("123");
            var val = <%= this.ForJSInt %>;
            alert(val);

            var val2 = <%= (this.ForJSBool) ? "true" : "false"%>;
            alert(val2);

            var val3 = '<%= this.ForJSString%>';
            alert(val3);

            function exec2() {
                var hf2 = document.getElementById("hf2");
                var val = hf2.value;
                alert(val);
            }
            exec2();


            function exec() {
                var lbl = document.getElementById("lblName");
                lbl.innerHTML = "No Hello";

                //var txt1 = document.getElementById("txt1");
                //txt1.value = "World"

                var hf1 = document.getElementById("hf1");
                hf1.value = "World";
            }
        </script>
    </form>
</body>
</html>
