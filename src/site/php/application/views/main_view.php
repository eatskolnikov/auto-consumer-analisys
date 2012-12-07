<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=iso-8859-1">
		<title>PUCMM Proyecto Final - Auto Consumer Analysis</title>
		<link href="<?=base_url() ?>public/css/bootstrap.min.css" rel="stylesheet" />
		<link href="<?=base_url() ?>public/css/style.css" rel="stylesheet" />
	</head>
	<body data-spy="scroll" data-offset="50">
		<div class="navbar navbar-fixed-top">
			<div class="navbar-inner">
				<div class="container">
					<a class="brand" href="#">Auto Consumer Analysis</a>
					<ul class="nav">
						<li class="active"><a href="#Descripcion">Descripci&oacute;n</a></li>
						<li><a href="#Reportes">Reporte de actividades</a></li>
						<li><a href="#Equipo">Equipo de trabajo</a></li>
						<li><a href="#Recursos">Recursos Externos</a></li>
					</ul>
				</div>
			</div>
		</div>
		<div class="container">
			<div class="row-fluid" >
				<section id="Descripcion" class="row-fluid well">
				<div class="page-header"><h1>Descripci&oacute;n del proyecto</h1><small></small></div>
				<h2>Visi&oacute;n y Objetivos</h2>
				<p>Nuestra visión en el proyecto es, poder lograr una innovación en los estudios de mercados, que se saque el máximo provecho al mismo, tener un proyecto que sea acogido entre las empresas y que ayude a mejorar y a crecer a las que lo implementen.</p>
				<p>Nuestros objetivos son, que sea implementado en varias plazas comerciales del país, que sea un producto de calidad, terminar el proyecto en su totalidad cumpliendo con todos los requerimientos en el tiempo establecido.</p>
				<h2>Alcance del Proyecto</h2>
				<p>En la fase de implementación, vamos a aplicar el proyecto en un área pequeña.</p>
				<p>Lo que va a hacer:</p>
				<ul><li>Leer la ubicación de un individuo a través de su celular y guardar el recorrido que tome dentro del local.</li>
				<li>Mostrar los datos estadísticos por medio de reportes a través de una aplicación.</li></ul>
				<p>Lo que no va a hacer:</p>
				<ul><li>Tomar información personal.</li>
				<li>Intervenir llamadas.</li>
				<li>Enviar mensajes a los clientes de las plazas.</li></ul>
				</section>
				<section id="Reportes" class="row-fluid well">
				<div class="page-header"><h1>Reporte de actividades</h1><small></small></div>
				<h2>Detección de dispositivos bluetooth<small>(01/09/2012)</small> </h2>
				<p>Con el módulo de bluetooth del arduino UNO podemos ver los dispositivos cercanos</p>
				<h2>Nos mudamos a Trello <small>(01/09/2012)</small> </h2>
				<p>Ahora usaremos Trello para dar seguimiento a los requerimientos aparte del Microsoft Project, para seguimiento de las tareas ver <a href="https://trello.com/board/proyecto-pucmm/503be5e0b4509c837f602e10">aqu&iacute;</a></p>
				<h2>Diseño de la página <small>(03/09/2012)</small> </h2>
				<p>Se escogió y se implementó el diseño de la página web del proyecto</p>
				<h2>Nuevo Repositorio en Github <small>(06/09/2012)</small> </h2>
				<p>Creamos un repositorio del proyecto en Github. <a href="https://github.com/eatskolnikov/auto-consumer-analisys">Link al repositorio</a> </p>
				<h2>Comienzo de pruebas con dispositivos de red<small>(14/10/2012)</small> </h2>
				<p><ul>
					<li>Compra de switch de 5 puertos para pruebas</li>
					<li>Integramos el modulo de ethernet con el arduino UNO</li>
					<li>Realizando el programa que se comunique con los arduinos</li>
				</ul></p>
				<h2>Programa que se comunica con los arduinos terminado<small>(11/11/2012)</small> </h2>
				</section>
				<section id="Equipo" class="row-fluid well">
					<div class="page-header"><h1>Equipo de trabajo</h1><small></small></div>
					<h2>Máximo Martinez <small>Profesor Guía</small></h2>
					<h2>Gustavo Betances <small>2007-4444</small></h2>
					<h2>Enmanuel Toribio <small>2007-6332</small></h2>
				</section>
				<section id="Recursos" class="row-fluid well">
					<div class="page-header"><h1>Recursos Externos</h1><small></small></div>
					<ul>
						<li><a href="https://trello.com/board/proyecto-pucmm/503be5e0b4509c837f602e10">Trello</a></li>
						<li><a href="https://github.com/eatskolnikov/auto-consumer-analisys">Repositorio en GitHub</a></li>
					</ul>
				</section>
			</div>
			<div id="footer" class="row-fluid well" style="text-align:center" >
				Design powered by <a href="http://twitter.github.com/bootstrap/">Twitter Bootstrap</a>
			</div>
		</div>
		<script type="text/javascript" src="<?=base_url() ?>public/js/jquery.min.js"></script>
		<script type="text/javascript" src="<?=base_url() ?>public/js/bootstrap.min.js"></script>
		<script type="text/javascript" src="<?=base_url() ?>public/js/bootstrap-scrollspy.js"></script>
		<script type="text/javascript" src="<?=base_url() ?>public/js/main.js"></script>
	</body>
</html>