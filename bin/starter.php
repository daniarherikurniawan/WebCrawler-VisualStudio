<!DOCTYPE html>
<body>
<?php
	extract($_GET);
	
	if (isset($algoritma))
		$code = ($algoritma=="DFS") ? 1 : 0;
	else
		$code = 0;
	
	if (!isset($depth)) $depth = 0;
	
	if (!isset($urlList))
		$urlList = null;
	
	$query = "crawler.exe ";
	$query = $query." ".$code;
	$query = $query." ".$depth;
	if ($urlList != null) {
		foreach ($urlList as $url) {
			$query = $query.' "'.$url.'"';
		}
	}
	
	set_time_limit(0);
	exec($query);
	header("Location: index.html");
?>
</body>