<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');
	
	// check that connection happenned
	if(mysqli_connect_errno()){
		echo "1: Connection failed"; // error code #1 = connection failed
		exit();
	}
	
	$username = $_POST["name"];
	$password = $_POST["password"];

	// if name already exists in the db
	$nameCheckQuery = "SELECT username FROM players WHERE username='" . $username . "';";

	$nameCheck = mysqli_query($con, $nameCheckQuery) or die("2: Name check query failed");  //error code #2 = name check query failed

	// at least one user found in previous query
	if(mysqli_num_rows($nameCheck) > 0){
		echo "3: Name already exists"; // error code #3 = name exists cannot register
		exit();
	}

	// add user to the table
	$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
	$hash = crypt($password, $salt);
	$insertUserQuery = "INSERT INTO players (username, hash, salt) VALUES ('".$username."','".$hash."','".$salt."');";

	mysqli_query($con, $insertUserQuery) or die("4: Insert player query failed"); // error code #4 = insert player query failed

	echo("0");

?>