<?php

    $con = mysqli_connect('localhost', 'root', '', 'unityaccess');
	 
	// check that connection happened
	if(mysqli_connect_errno()){
		echo "1: Connection failed"; // error code #1 = connection failed
		exit();
	}
	
	$username = mysqli_real_escape_string($con, $_POST["name"]);
	$usernameClean = filter_var($username, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
	$password = $_POST["password"];

	// if name already exists in the db
	$nameCheckQuery = "SELECT username, salt, hash, score FROM players WHERE username='" . $usernameClean . "';";

	$nameCheck = mysqli_query($con, $nameCheckQuery) or die("2: Name check query failed");  //error code #2 = name check query failed

	if(mysqli_num_rows($nameCheck) != 1){
		echo "5: Either no user with name, or more than one"; //error #5 = number of names matching != 1
		exit();
	}

	// get login info from query
	$existingInfo = mysqli_fetch_assoc($nameCheck);
	$salt = $existingInfo["salt"];
	$hash = $existingInfo["hash"];

	$loginhash = crypt($password, $salt);

	if($hash != $loginhash){
		echo "6: Incorrect password."; // error code #6 = passwoord does not hash to match table
		exit();
	}

	echo "0\t".$existingInfo["score"];
?>