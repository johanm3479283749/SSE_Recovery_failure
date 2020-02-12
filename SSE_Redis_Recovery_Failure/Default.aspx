<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SSE_Recovery_Failure.DefaultPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="/js/default.js"></script>
</head>
<body>
    <div>
        <table>
            <tr>
                <td>
                    <input type="text" id="SSE_Message"/>
                    <button onclick="submitMessage" title="Send"></button>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
