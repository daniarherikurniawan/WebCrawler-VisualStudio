<!DOCTYPE html>
<html>
	<head>
		<title>Search Result</title>
		<link rel="stylesheet" href="bootstrap/css/bootstrap.css">
		<link rel="stylesheet" href="css/result.css">		
		<?php 
			session_start();
			if (isset($_SESSION['keywords']))
				$keywords = $_SESSION['keywords'];
			else
				$keywords = "";
			
			if (isset($_SESSION['pageResult']))
				$results = (int)$_SESSION['pageResult'][0];
			else
				$results = 0;
				
			if (isset($_SESSION['currentPage']))
				$page = $_SESSION['currentPage'];
			else
				$page = 0;
			
			$maxPage = (int)floor(($results-1)/10 + 1);
			if ($maxPage < 0) $maxPage = 0;
		?>
	</head>
	<body>
			<div class="col-xs-12 col-sm-offset-1 col-sm-10">
				<!-- Logo -->
				<div class=" col-xs-12 logo">
					<a href="index.php"><img src="image/logo.png"  alt="Back to Home" class="img-responsive center-block" alt="The Crawler"></a>
				</div>

				<!-- Search Box -->
				<div class="clearfix"></div>
				<div class="col-xs-12">
					<form action="executor.php" method="GET">
						<div class="form-group has-warning has-feedback">
							<label class="control-label" for="inputSuccess2"> Cari sesuatu</label>
							<input name="q" type="text" class="form-control search-box" id="inputSuccess2" value="<?php echo htmlentities($keywords); ?>">
							<input name="p" type="hidden" value=0>
							<button type="submit" class="glyphicon glyphicon-search form-control-feedback">
							</button>
						</div>
					</form>	
				</div>
				
				<div class="clearfix"></div>
				<div class="col-xs-12">
					<!-- Hasil Penelusuran & Pagination-->
					<div class="panel panel-warning">
						<!-- Hasil Penelusuran -->
				 		<div class="panel-heading">Hasil penelusuran untuk "<?php echo htmlentities($keywords); ?>" - <?php echo $results; ?> ditemukan</div>
						<div class="panel-body">
							<?php
								$con=mysqli_connect("localhost","root","","db_crawler");
								// Check connection
								if (mysqli_connect_errno()) {
									echo "Failed to connect to MySQL: " . mysqli_connect_error();
								}
								
								if (isset($_SESSION['pageResult'])) {
									$pageResult = $_SESSION['pageResult'];
									$upper = count($pageResult);
									for ($i = 1; $i < $upper; ++$i) {
										$result = mysqli_query($con,sprintf("SELECT title, link FROM visited_links WHERE id=%d", $pageResult[$i]));
										while ($row = mysqli_fetch_array($result)) {
											echo '<div class="clearfix"></div>';
											echo '<div class="col-xs-12 text-left">';
											echo '<p class="judul"><a href="'.$row['link'].'">'.$row['title'].'</a></p>';
											echo '<p>'.$row['link'].'</p>';
											echo '</div>';
										}
									}
								}
							?>
						</div>
						<!-- Pagination -->
						<div class="panel-footer">
							<div class="text-center">
								<ul class="pagination">
									<?php
										if ($page > 0)
											echo sprintf('<li><a href="executor.php?p=%d">&laquo;</a></li>', $page-1);
										$awal = $page-4;
										if ($awal < 0) $awal = 0;
										for ($i = $awal; $i < $maxPage && $i < $awal+9; ++$i) {
											if ($i == $page)
												echo sprintf('<li class="active"><a>%d</a></li>', $i+1);
											else
												echo sprintf('<li><a href="executor.php?p=%d">%d</a></li>', $i, $i+1);
										}
										if ($page < $maxPage-1)
											echo sprintf('<li><a href="executor.php?p=%d">&raquo;</a></li>', $page+1);
									?>
								</ul>
							</div>
						</div>
					</div>
				</div>

				<!-- Footer -->
				<div class="clearfix"></div>
				<div class="text-center col-xs-12 copyright">
					<strong>&copy; Copyright 2014 The Crawler Team</strong>
				</div>
			</div>
	</body>
</html>