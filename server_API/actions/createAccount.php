<?php

header('Content-Type: application/json');
include_once '../config/Database.php';
include_once '../objects/User.php';

$json = json_decode(file_get_contents('php://input'), true);

$database = new Database();
$db = $database->getConnection();

$user = new User($db);

if (isset($json['username']) and isset($json['password']) and isset($json['email'])) {
	$username = htmlspecialchars($json['username']);
	$password = htmlspecialchars($json['password']);
	$email = htmlspecialchars($json['email']);
	$firstname = htmlspecialchars($json['firstName']);
	$lastname = htmlspecialchars($json['lastName']);
	$phone = htmlspecialchars($json['phone']);
	$birthDate = htmlspecialchars($json['birthDate']);
	$sexe = htmlspecialchars($json['sexe']);
	$address = htmlspecialchars($json['address']);
	$email_verification_token = md5(uniqid(rand(), true));


	$result = $user->create($username, $email, $password, $firstname, $lastname, $phone, $birthDate, $sexe, $address, $email_verification_token);
	$result["user"] = $user->getUser($username);
	echo json_encode($result);
} else {
	$result["success"] = false;
	$result["error"] = "Veuillez remplir tous les champs";
	echo json_encode($result);
}

?>
