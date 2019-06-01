<?php

    $con = mysqli_connect('localhost', 'root', '', 'unityaccess');
	 
	// check that connection happened
	if(mysqli_connect_errno()){
		echo "1: Connection failed"; // error code #1 = connection failed
		exit();
	}
	
	$username = $_POST["name"];
	$newScore = $_POST["score"];

	// double check there is only one user with this name
	$nameCheckQuery = "SELECT username FROM players WHERE username='" . $username . "';";

	$nameCheck = mysqli_query($con, $nameCheckQuery) or die("2: Name check query failed");  //error code #2 = name check query failed

	if(mysqli_num_rows($nameCheck) != 1){
		echo "5: Either no user with name, or more than one"; //error #5 = number of names matching != 1
		exit();
	}

	$updateQuery = "UPDATE players SET score = " . $newScore . " WHERE username = '" . $username . "';";

	mysqli_query($con, $updateQuery) or die("7: Save query failed"); // error code #7 - Update query failed

	echo "0";

?>