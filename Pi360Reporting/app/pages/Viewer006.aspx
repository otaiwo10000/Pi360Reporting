﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer006.aspx.cs" Inherits="Pi360Reporting.app.pages.Viewer006" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form006" runat="server">
   <asp:ScriptManager ID="ScriptManager006" runat="server">
        </asp:ScriptManager>
 
        <div style="height: 600px;">
            <rsweb:ReportViewer ID="reportViewer006" runat="server" Width="100%" Height="100%" ShowParameterPrompts="false" AsyncRendering="false" SizeToReportContent="true" ShowPageNavigationControls="true" KeepSessionAlive="true"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
