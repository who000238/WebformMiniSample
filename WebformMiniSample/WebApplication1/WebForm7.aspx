<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="WebApplication1.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>My Title</title>

    <style>
        p:last-child{
            
        }
        span{
            color:green; 
        }
        .cls1{
            color:yellow; background-color:antiquewhite
        }
        .cls2{
            color:blue; background-color:aquamarine
        }

    </style>
</head>
<body>
    <div>
        <p class="cls1">P Text1</p>
        <p>
            <span class="cls1">First</span>
            <span class="cls1">Second</span>
            <span class="cls2">Third</span>
        </p>
        <p class="cls2">P Text3</p>
    </div>
</body>
</html>
