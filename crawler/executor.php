<?php
	session_start();
	if (isset($_GET['q']))
		$keywords = $_GET['q'];
	elseif (isset($_SESSION['keywords']))
		$keywords = $_SESSION['keywords'];
	else
		$keywords = "";
		
	if (isset($_GET['p']))
		$page = $_GET['p'];
	else
		$page = 0;
	
	$query = sprintf("QueryMaker.exe %d %s", $page, $keywords);
	exec($query, $res);
	
	$_SESSION['pageResult'] = $res;
	$_SESSION['keywords'] = $keywords;
	$_SESSION['currentPage'] = $page;
	
	header("Location: result.php");
?>