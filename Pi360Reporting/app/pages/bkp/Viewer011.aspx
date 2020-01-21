<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer011.aspx.cs" Inherits="Pi360Reporting.app.pages.Viewer011" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form011" runat="server">
   <asp:ScriptManager ID="ScriptManager011" runat="server">
        </asp:ScriptManager>
 
        <div style="height: 1000px;">
            <rsweb:ReportViewer ID="reportViewer011" runat="server" Width="100%" Height="1000px" ShowParameterPrompts="false" SizeToReportContent="true"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
