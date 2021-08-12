<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryJquery2.aspx.cs" Inherits="WebApplication2.TryJquery2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="JS/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $(".pp").click(function () {
                $(this).hide(0.0001);   //show.hide可以加入參數slow.fast 或直接給予數值(毫秒)
            });
            $("#btn1").on("click", function () {
                $(".pp").show(1500).css("color", "blue").css("font-size", "24pt").css("background-color","yellow"); //chaining 串接
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>If you click on me, I will disappear.</p>
            <p class="pp">Click me away!</p>
            <p class="pp">Click me away!</p>
            <p class="pp">Click me away!</p>
            <p class="pp">Click me away!</p>
            <p class="pp">Click me away!</p>
            <p class="pp">Click me away!</p>
            <p class="pp">Click me away!</p>
            <p class="pp" id="p1">Click me too!</p>
            <input type="text" id="txt1" />
            <button type="button" id="btn1">Click me</button>
        </div>
    </form>
</body>
</html>
