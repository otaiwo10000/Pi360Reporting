<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer004.aspx.cs" Inherits="Pi360Reporting.app.pages.Viewer004" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form004" runat="server">
   <asp:ScriptManager ID="ScriptManager004" runat="server">
        </asp:ScriptManager>
 
        <div style="height: 600px; width: 2000px;">
            <rsweb:ReportViewer ID="reportViewer004" runat="server" Width="100%" Height="100%" ShowParameterPrompts="false" AsyncRendering="false" SizeToReportContent="true" ZoomMode="FullPage" ShowPageNavigationControls="true" ></rsweb:ReportViewer>
                    
<%--            <rsweb:ReportViewer ID="reportViewer004" runat="server" Width="100%" Height="100%" ShowParameterPrompts="false" ZoomMode="FullPage"></rsweb:ReportViewer>--%>

        </div>
    </form>
</body>
</html>
