<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeviceForm.aspx.cs" Inherits="acawebclient.ajax_service.forms.DeviceForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../public/css/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="frmProps" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server"></asp:UpdatePanel>
        <div class="row-fluid">
        <table>
        <tr><td>Ip </td><td><input ClientIDMode="Static" Id="tbxIp" name="Ip" runat="server" type="text" class="input-mini" /></td></tr>
        <tr><td>LatLng</td><td><input ClientIDMode="Static" Id="tbxLatLng" name="LatLng" runat="server" type="text" readonly="readonly" class="input-mini"/></td></tr>
        <tr>
            <td>Description</td>
            <td><textarea class="input-mini" ClientIDMode="Static" Id="tbxDescription" name="Description" rows="3" runat="server"></textarea></td>
        </tr>
        <tr>
            <td><asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_OnClick" class="btn" Text="Update" /></td>
            <td><asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_OnClick" class="btn" Text="Delete" /></td>
        </tr>
        </table>
        <input ClientIDMode="Static" Id="hdnDeviceId" name="DeviceId" runat="server" type="hidden" />
        </div>
    </form>
    <script type="text/javascript" src="../../public/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../public/js/bootstrap.min.js"></script>

</body>
</html>
