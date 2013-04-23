<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="acawebclient.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Auto consumer analisys - Web Application</title>
    <link href="/public/css/bootstrap.min.css" rel="stylesheet"/>
	<style type="text/css">
		html { height: 100% }
		body { height: 100%; margin: 0; padding: 0 }
		#map_canvas { min-height: 728px; height: 100% }
	</style>
</head>
<body>
    <div class="container-fluid">
        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner">
            <a class="brand" href="#">Auto Consumer Analisys</a>
            <ul class="nav">
            </ul>
            </div>
        </div>
        <div class="row-fluid" style="margin-top: 50px">
            <div class="offset4 span4 hero-unit">
            <form id="form1" runat="server" class="form-horizontal">
                <h4>Autenticaci&oacute;n de usuario</h4>
                <p><asp:TextBox Id="tbxUsername" placeholder="Username" class="input-large" runat="server"></asp:TextBox></p>
                <p><asp:TextBox TextMode="Password" Id="tbxPassword" placeholder="Password" class="input-large" runat="server"/></p>
                <p><asp:Button Text="Entrar" class="btn btn-primary" runat="server" OnClick="LogIn"/><asp:Label runat="server" ID="lblMessage" ForeColor="#ff0000" Font-Size="10px" /></p>
            </form>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/public/js/jquery.min.js" ></script>
    <script type="text/javascript" src="/public/js/bootstrap.min.js" ></script>
</body>
</html>
