<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryJquery.aspx.cs" Inherits="WebApplication2.TryJquery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="JS/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {  //可以用 $(function() { 代替
        //$(function () {
            $(".pp").click(function () {
                $(this).hide();
            });
            //$("#p1").click(function () {
            //    $(".pp").show();
            //});
            $("#txt1").change(function () {
                alert($(this).val());
            });
            $("#btn1").click(function () {
                $("#txt1").val('');
            });
            //$("#btn1").click(function () {     //click事件可以疊加
            //    alert(123);
            //});
            $("#btn1").on("click", function () {
                alert(123);
            });
            $("#btn1").mouseenter(function () {
                alert("You entered p1!!");
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
