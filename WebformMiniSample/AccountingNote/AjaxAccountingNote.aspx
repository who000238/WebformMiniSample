﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxAccountingNote.aspx.cs" Inherits="AccountingNote.AjaxAccountingNote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="JS/jquery-3.6.0.min.js"></script>
    <script>
        $(function () {
            $("#btnSave").click(function () {
                var id = $("#hfID").val(); 
                var actType = $("#ddlActType").val();
                var amount = $("#txtAmount").val();
                var caption = $("#txtCaption").val();
                var desc = $("#txtDesc").val();

                if (id) {
                    $.ajax({
                        url: "http://localhost:64683/Handlers/AccountingNoteHandler.ashx?ActionName=update",
                        type: "POST",
                        data: {
                            "Caption": caption,
                            "Amount": amount,
                            "ActType": actType,
                            "Body": desc
                        },
                        success: function (result) {
                            alert("更新成功");
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "http://localhost:64683/Handlers/AccountingNoteHandler.ashx?ActionName=create",
                        type: "POST",
                        data: {
                            "Caption": caption,
                            "Amount": amount,
                            "ActType": actType,
                            "Body": desc
                        },
                        success: function (result) {
                            alert("新增成功");
                        }
                    });
                }
            });

            $("#btnRead").click(function () {
                $.ajax({
                    url: "http://localhost:64683/Handlers/AccountingNoteHandler.ashx?ActionName=query",
                    type: "POST",
                    data: {
                        "ID": 28,
                    },
                    success: function (result) {
                        $("#hfID").val(result["ID"]);
                        $("#ddlActType").val(result["ActType"]);
                        $("#txtAmount").val(result["Amount"]);
                        $("#txtCaption").val(result["Caption"]);
                        $("#txtDesc").val(result["Body"]);
                    }
                });
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hfID" />
        <button type="button" id="btnRead">Read Data</button>
        <table>
            <tr>
                <td>
                    <%-- 這裡放主要內容 --%>
                    Type:
                    <select id="ddlActType">
                        <option value="0">支出</option>
                        <option value="1">收入</option>
                    </select>
                    <br />
                    Amount:
                              <input type="number" id="txtAmount" />
                    <br />
                    Caption:
                             <input type="text" id="txtCaption" />
                    <br />
                    Desc:
                               <textarea id="txtDesc" rows="5" cols="60"></textarea>
                    <br />
                    <button type="button" id="btnSave">Save</button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
