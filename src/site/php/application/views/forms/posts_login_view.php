<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=iso-8859-1">
		<title>PUCMM Proyecto Final - Auto Consumer Analysis</title>
		<link href="<?=base_url();?>public/css/bootstrap.min.css" rel="stylesheet" />
		<link href="<?=base_url();?>public/css/style.css" rel="stylesheet" />
	</head>
	<body data-spy="scroll" data-offset="50">
		<div class="navbar navbar-fixed-top">
			<div class="navbar-inner">
				<div class="container">
					<a class="brand" href="#">Auto Consumer Analysis</a>
				</div>
			</div>
		</div>
		<div class="container">
			<div class="row-fluid" >
				<section id="Descripcion" class="row-fluid well">
				<div class="page-header"><h1>Login</h1></div>
				<form action="<?=base_url() ?>posts/login" method="post">
					<label for="username">Username</label><input type="text" name="username" /><br/>
					<label for="password">Password</label><input type="password" name="password" />
					<input type="hidden" name="form_sent" value="1"/>
					<input type="submit" />
				</form>
				</section>
			</div>
		</div>
		<script type="text/javascript" src="<?=base_url();?>public/js/jquery.min.js"></script>
		<script type="text/javascript" src="<?=base_url();?>public/js/bootstrap.min.js"></script>
		<script type="text/javascript" src="<?=base_url();?>public/js/main.js"></script>
	</body>
</html>