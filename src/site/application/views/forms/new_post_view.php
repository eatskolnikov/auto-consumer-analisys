<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=iso-8859-1">
		<title>PUCMM Proyecto Final - Auto Consumer Analysis</title>
		<link href="<?=base_url();?>public/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
		<link href="<?=base_url();?>public/css/style.css" rel="stylesheet" type="text/css" />
		<link href="<?=base_url();?>public/css/jquery.wysiwyg.css" rel="stylesheet" type="text/css">
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
				<div class="page-header"><h1>Nuevo post</h1></div>
				<form action="<?=base_url() ?>posts/add" method="post">
					<label for="title">Titulo</label><input type="text" name="title"  class="input-medium search-query" /><br/>
					<textarea id="wysiwyg" name="body" ></textarea>
					<label for="date">Fecha</label><input type="text" name="date">
					<input type="hidden" name="form_sent" value="1"/>
					<input type="submit" />
				</form>
				</section>
			</div>
		</div>
		<script type="text/javascript" src="<?=base_url();?>public/js/jquery.min.js"></script>
		<script type="text/javascript" src="<?=base_url();?>public/js/jquery.wysiwyg.js"></script>
		<script type="text/javascript" src="<?=base_url();?>public/js/wysiwyg.image.js"></script>
		<script type="text/javascript" src="<?=base_url();?>public/js/wysiwyg.link.js"></script>
		<script type="text/javascript" src="<?=base_url();?>public/js/wysiwyg.table.js"></script>
		<script type="text/javascript">
		(function($) {
			$(document).ready(function() {
				$('#wysiwyg').wysiwyg({
				  controls: {
					bold          : { visible : true },
					italic        : { visible : true },
					underline     : { visible : true },
					strikeThrough : { visible : true },
					
					justifyLeft   : { visible : true },
					justifyCenter : { visible : true },
					justifyRight  : { visible : true },
					justifyFull   : { visible : true },

					indent  : { visible : true },
					outdent : { visible : true },

					subscript   : { visible : true },
					superscript : { visible : true },
					
					undo : { visible : true },
					redo : { visible : true },
					
					insertOrderedList    : { visible : true },
					insertUnorderedList  : { visible : true },
					insertHorizontalRule : { visible : true },

					h4: {
						visible: true,
						className: 'h4',
						command: ($.browser.msie || $.browser.safari) ? 'formatBlock' : 'heading',
						arguments: ($.browser.msie || $.browser.safari) ? '<h4>' : 'h4',
						tags: ['h4'],
						tooltip: 'Header 4'
					},
					h5: {
						visible: true,
						className: 'h5',
						command: ($.browser.msie || $.browser.safari) ? 'formatBlock' : 'heading',
						arguments: ($.browser.msie || $.browser.safari) ? '<h5>' : 'h5',
						tags: ['h5'],
						tooltip: 'Header 5'
					},
					h6: {
						visible: true,
						className: 'h6',
						command: ($.browser.msie || $.browser.safari) ? 'formatBlock' : 'heading',
						arguments: ($.browser.msie || $.browser.safari) ? '<h6>' : 'h6',
						tags: ['h6'],
						tooltip: 'Header 6'
					},
					
					cut   : { visible : true },
					copy  : { visible : true },
					paste : { visible : true },
					html  : { visible: true },
					increaseFontSize : { visible : true },
					decreaseFontSize : { visible : true },
					exam_html: {
						exec: function() {
							this.insertHtml('<abbr title="exam">Jam</abbr>');
							return true;
						},
						visible: true
					}
				  }
				});
			});
		})(jQuery);
		</script>
		<script type="text/javascript" src="<?=base_url();?>public/js/bootstrap.min.js"></script>
	</body>
</html>