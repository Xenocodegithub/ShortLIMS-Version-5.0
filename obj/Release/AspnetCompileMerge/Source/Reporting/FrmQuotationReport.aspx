<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmQuotationReport.aspx.cs" Inherits="LIMS_DEMO.Reporting.FrmQuotationReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">

        function hideMenu()
        {
            debugger;
            var sel = $("select#rvReportViewer_ctl05_ctl04_ctl00_Menu");
            $("#rvReportViewer_ctl05_ctl04_ctl00_ButtonImg a").remove();
            //sel.find("option[value='Excel']").remove();
            //sel.find("option[value='CSV']").remove();
            //sel.find("option[value='IMAGE']").remove();
            //sel.find("option[value='MHTML']").remove();
            //sel.find("option[value='PDF']").remove();
            //sel.find("option[value='EXCEL']").remove();
        }
        
    </script>
</head>
<body onload="hideMenu()">
    <form id="form1" runat="server">
       <div id="rightcontent">
            <%--<asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>     --%>
            <%--<asp:Label ID="lblError" runat="server" ForeColor="Red">Error: none</asp:Label>--%>
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="rvReportViewer" Width="950px" Height="800px" runat="server" OnLoad="rvReportViewer_Load">
            </rsweb:ReportViewer>  <%--OnPreRender="rvReportViewer_PreRender"--%>
            <br />
        </div>
    </form>
</body>
</html>
