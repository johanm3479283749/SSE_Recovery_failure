<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.DefaultPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="/js/default.js"></script>
    <script src="/api/js/ss-utils.js"></script>
</head>
<body onload="initSSE();">
    <div>
        <p>
            1. Install and run docker desktop app<br/>
            2. Cmd-prompt: In the root, run "docker-compose up" to start redis<br/>
            3. Send a few messages to ensure Redis is running<br/>
            4. Cmd-prompt: Turn redis off (ctrl c+c)<br/>
            5. Send a few messages which will cause and exception<br/>
            6. Cmd-prompt: Turn redis on again ("docker-compose up")<br/>
            7. Send a few messages<br/>
            <br/>
            Result: SSE is unresponsive<br/>
            <br/>
            Reloading page wont help either<br/>
            <br/>
        </p>
    </div>    
    <div>
        <table>
            <tr>
                <td>
                    <input type="text" id="SSE_Message" value="My message"/>
                    <button onclick="submitMessage();">Submit message</button>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="messageTable">
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
