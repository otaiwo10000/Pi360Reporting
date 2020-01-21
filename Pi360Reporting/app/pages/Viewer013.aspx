<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer013.aspx.cs" Inherits="Pi360Reporting.app.pages.Viewer013" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form013" runat="server">
   <asp:ScriptManager ID="ScriptManager013" runat="server">
        </asp:ScriptManager>
 
        <div style="height: 600px;">
            <!--<rsweb:ReportViewer ID="reportViewer013" runat="server" Width="100%" Height="100%" ShowParameterPrompts="false"></rsweb:ReportViewer>-->
			<rsweb:ReportViewer ID="reportViewer013" runat="server" Width="100%" Height="100%" ShowParameterPrompts="false" AsyncRendering="false" SizeToReportContent="true" ShowPageNavigationControls="true" KeepSessionAlive="true"></rsweb:ReportViewer>

        </div>
    </form>
</body>
</html>
