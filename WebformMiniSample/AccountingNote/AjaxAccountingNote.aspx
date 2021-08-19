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

            //$("#btnRead").click(function () {
            //    $.ajax({
            //        url: "http://localhost:64683/Handlers/AccountingNoteHandler.ashx?ActionName=query",
            //        type: "POST",
            //        data: {
            //            "ID": 28,
            //        },
            //        success: function (result) {
            //            $("#hfID").val(result["ID"]);
            //            $("#ddlActType").val(result["ActType"]);
            //            $("#txtAmount").val(result["Amount"]);
            //            $("#txtCaption").val(result["Caption"]);
            //            $("#txtDesc").val(result["Body"]);
            //        }
            //    });
            //});

            $(document).on("click", ".btnReadData", function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "http://localhost:64683/Handlers/AccountingNoteHandler.ashx?ActionName=query",
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },
                    success: function (result) {
                        $("#hfID").val(result["ID"]);
                        $("#ddlActType").val(result["ActType"]);
                        $("#txtAmount").val(result["Amount"]);
                        $("#txtCaption").val(result["Caption"]);
                        $("#txtDesc").val(result["Body"]);

                        $("#divEditor").show(3000);

                    }
                });
            });

            $("#btnAdd").click(function () {
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").show(3000);
            });

            $("#btnCancle").click(function () {
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").hide(3000);

            });

            $("#divEditor").hide();

            $.ajax({
                url: "http://localhost:64683/Handlers/AccountingNoteHandler.ashx?ActionName=list",
                type: "GET",
                data: {},

                success: function (result) {
                    var table = '<table border="1">';
                    table += '<tr><th>Caption</th><th>Amount</th><th>ActType</th><th>CreateDate</th><th>Act</th></tr>';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr>                                                      
                                    <td>${obj.Caption}</td>
                                    <td>${obj.Amount}</td>
                                    <td>${obj.ActType}</td>
                                    <td>${obj.CreateDate}</td>
                                    <td>
                                            <input type="hidden" class="hfRowID" value="${obj.ID}"/>
                                            <button type="button" class="btnReadData">
                                            <img src="Images/search.png" width="20" />
                                            </button>
                                    </td>
                                </tr > `;
                        table += htmlText
                    }   /*字串補差點是用TAB上面的``符號*/

                    table += "</table>";
                    $("#divAccountingList").append(table);
                }
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divEditor">
            <input type="hidden" id="hfID" />
            <%--<button type="button" id="btnRead">Read Data</button>--%>
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
                        <button type="button" id="btnCancle">Cancle</button>
                    </td>
                </tr>
            </table>
        </div>
        <button type="button" id="btnAdd">Add</button>
        <div id="divAccountingList">
        </div>
    </form>
</body>
</html>
