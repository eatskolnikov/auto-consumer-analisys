﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="acawebclient.Default" %>

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
        </ul>
        </div>
    </div>
    <div class="row-fluid">
    <form id="form1" runat="server" class="form-horizontal">
        <input name="username" id="username" placeholder="Username" class="input-medium"/><br/>
        <input name="password" id="password" placeholder="Password" class="input-medium"/><br/>
        <input type="submit" value="Log in" class="btn-primary"/>
    </form>
    </div>
    <script type="text/javascript" src="/public/js/jquery.min.js" ></script>
    <script type="text/javascript" src="/public/js/bootstrap.min.js" ></script>
</body>
</html>
