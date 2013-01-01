<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="acawebclient.Default"  %>
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
    <div class="navbar">
        <div class="navbar-inner">
        <a class="brand" href="#">Auto Consumer Analisys</a>
        <ul class="nav">
            <li class="active menu-option"><asp:HyperLink href="/" runat="server" ID="lnkMap">Map</asp:HyperLink></li>
            <li><asp:HyperLink href="/Reports.aspx" runat="server" ID="HyperLink1">Reports</asp:HyperLink></li>
            <%--<li><a href="#/help">Help</a></li>--%>
        </ul>
        </div>
    </div>
    <div class="row-fluid">
<div id="map_canvas"></div>    
    </div>
    <script type="text/javascript">
        var base_url = '/';
        var map = '';
    </script>
    <script type="text/javascript" src="/public/js/jquery.min.js" ></script>
    <script type="text/javascript" src="/public/js/bootstrap.min.js" ></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA8XhsuuGBy8fBxJM1H8fO-p13zWdMoaWg&sensor=false"></script>
    <script type="text/javascript" src="/public/js/mapsettings.js" ></script>
</body>
</html>

