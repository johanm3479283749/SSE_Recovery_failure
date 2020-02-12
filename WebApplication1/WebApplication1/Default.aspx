<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.DefaultPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="/js/default.js"></script>
</head>
<body onload="subscribe();">
    <div>
        <table>
            <tr>
                <td>
                    <input type="text" id="SSE_Message"/>
                    <button onclick="submitMessage();">Submit message</button>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
