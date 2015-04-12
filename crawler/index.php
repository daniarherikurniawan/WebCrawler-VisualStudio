<!DOCTYPE html>
<html>
<head>
	<title>The Crawler</title>
	<link rel="stylesheet" href="bootstrap/css/bootstrap.css">
	<link rel="stylesheet" href="css/index.css">
	<link rel="stylesheet" href="onepagescroller/onepage-scroll.css">
	<?php
		session_start();
		if (isset($_SESSION['keywords']))
			$keywords = $_SESSION['keywords'];
		else
			$keywords = "";
	?>
</head>

<body>
	<div class="main">
			<!-- Homepage -->
			<section>
				<!-- Logo -->
				<div class="col-xs-12 col-sm-offset-1 col-sm-7 col-sm-offset-1 col-md-5 logo container">
					<a href="result.php">
						<div class="text-center">
						<img src="image/logo.png"  class="" alt="The Crawler">
						</div>
					</a>
				</div>

				<!-- Search Box -->
				<div class="clearfix"></div>
				<div class="col-xs-12 col-sm-offset-1 col-sm-7 col-sm-offset-1 col-md-5">
					<form action="executor.php" method="GET">
						<div class="form-group has-warning has-feedback">
							<label class="control-label" for="inputSuccess2"> Cari sesuatu</label>
							<input name="q" type="text" class="form-control search-box" id="inputSuccess2" value="<?php echo htmlentities($keywords); ?>">
							<input name="p" type="hidden">
							<a type="submit">
								<span class="glyphicon glyphicon-search form-control-feedback"></span>
							</a>
						</div>
					</form>	
				</div>

				<div class="clearfix"></div>
				<!-- Tombol "I'm Feeling Crawl" -->
				<a type="button" class="btn btn-lg crawl-btn col-xs-offset-2 col-xs-8 col-sm-offset-4 col-sm-5 col-md-4" href="#" onclick="goCrawl()">
					<span class="glyphicon glyphicon-hand-down"></span> I'm Feeling Like to Crawl
				</a>
				<!-- Tombol i(about) -->
				<a href="#" type="button" class="btn btn-lg goabout-btn" onclick="goAbout()">
					<span class="glyphicon glyphicon-info-sign"></span>
				</a>
			</section>

			<!-- Meet the Crawler -->
			<section>
				<div class="page-header col-xs-offset-1 col-xs-10 col-sm-offset-2 col-sm-8">
					<h2 class="text-center" >Meet The Crawler</h2>
				</div>

				<form action="starter.php" method="GET">
					<!-- Insert URL -->
					<div class="col-xs-offset-1 col-xs-10 col-sm-offset-1 col-sm-5 col-md-offset-1 col-md-6 form-horizontal">
						<div class="judul form-group text-right">
							<p>Insert url</p>
						</div>
						<div class="url-group">
							<div class="form-group" id="fg1">
								<!-- URL Textbox -->
								<label for="inputURL" class="control-label col-xs-3 col-sm-4 col-md-2">URL 1</label>
								<div class="col-xs-9 col-sm-8 col-md-10">
									<input name='urlList[]' type="text" id="inputURL" class="form-control">
								</div>						
							</div>
						</div>	
						<div class="text-right">
							<!-- Add & Remove URL's TextBox -->
							<a href="#" class="btn gocrawl-btn" id="removeUrl">-</a>
							<a href="#" class="btn gocrawl-btn" id="addUrl">+</a>
						</div>
					</div>

					<!-- Depth & Algoritma -->
					<div class="col-xs-offset-1 col-xs-10 col-sm-offset-0 col-sm-5 col-md-4 form-horizontal">
						<!-- Mode Algoritma -->
						<div class="form-group">
							<label class="control-label col-xs-3 col-sm-5 col-md-4">Algoritma</label>
							<!-- Radio BFS -->
							<label class="checkbox-inline">
								<input type="radio" name="algoritma" value="BFS" checked>
								BFS
							</label>
							<!-- Radio DFS -->
							<label class="checkbox-inline">
								<input type="radio" name="algoritma" value="DFS" >
								DFS
							</label>
						</div>

						<!-- Depth -->
						<div class="form-group">
							<label for="inputDepth" class="control-label col-xs-3 col-sm-5 col-md-4">Depth</label>
							<div class="col-xs-9 col-sm-7 col-md-8">
								<input name="depth" type="text" id="inputDepth" class="form-control">
							</div>
						</div>
						<!-- Tombol "Go Crawl" -->
						<div class="col-xs-12 col-sm-offset-7 col-sm-5 crawl">
							<button type="submit" class="btn gocrawl-btn btn-block"><span class="glyphicon glyphicon-off"></span> Go Crawl!</button>
						</div>
					</div>
				</form>
			</section>

			<!-- About -->
			<section>
				<div class="jumbotron">
				  <h1>About</h1>
				  <p>
				  	Nullam quis risus eget urna mollis ornare vel eu leo. Donec sed odio dui. Vestibulum id ligula porta felis euismod semper. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Nulla vitae elit libero, a pharetra augue.

				  	Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Aenean lacinia bibendum nulla sed consectetur. Sed posuere consectetur est at lobortis. Donec sed odio dui. Cras mattis consectetur purus sit amet fermentum.
				  </p>

				  <p class="hidden-xs">
				  	Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Donec id elit non mi porta gravida at eget metus.
				  </p>
				  <p><a class="btn btn-primary btn-lg about-btn" role="button" onclick="goHome()"><span class="glyphicon glyphicon-home"></span> Back to home</a></p>
				</div>
				
				<!-- Copyright & Creator -->
				<div class="col-xs-12 copyr">
					<div class="name col-xs-12">
						<div class="creator col-xs-4">Edmund</div>
						<div class="creator middle-creator col-xs-4">Eric</div>
						<div class="creator col-xs-4">Daniar</div>
					</div>
					<div class="clearfix"></div>
					&copy; Copyright 2014 The Crawler Team
				</div>
			</section>
	</div>	

	<!-- Load Javascript -->
	<script type="text/javascript" src="js/jquery.js"></script>
	<script type="text/javascript" src="onepagescroller/jquery.onepage-scroll.js"></script>
	<script type="text/javascript" src="js/script.js"></script>
</body>
</html>