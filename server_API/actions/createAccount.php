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

/*
header('Content-Type: application/json');

include_once '../config/Database.php';
$json = json_decode(file_get_contents('php://input'), true);

if (isset($json['username']) and isset($json['password']) and isset($json['email'])) {
	$username = htmlspecialchars($json['username']);
	$password = htmlspecialchars($json['password']);
	$email = htmlspecialchars($json['email']);
	$passwordHashed = password_hash($password, PASSWORD_DEFAULT); // encryptage du mot de passe
	// echo $password;
	$success = "";

	// Connexion à la base de données
	if ($username == "" or $password == "" or $email == "") {
		$result["success"] = false;
		$result["error"] = "Veuillez remplir tous les champs";
	} else {
		$checkIfUserExist = $bdd->prepare("SELECT * FROM infoperso WHERE username = ?"); // Requete pour vérifier si l'utilisateur existe déjà
		$checkIfUserExist->execute(array($username)); // Execution de la requête
		$checkIfEmailExist = $bdd->prepare("SELECT * FROM infoperso WHERE email = ?"); // Requete pour vérifier si l'email existe déjà
		$checkIfEmailExist->execute(array($email)); // Execution de la requête

		if ($checkIfUserExist->rowCount() > 0) {
			$result["success"] = false;
			$result["error"] = "Ce nom d'utilisateur est déjà utilisé";
		} else if ($checkIfEmailExist->rowCount() > 0) {
			$result["success"] = false;
			$result["error"] = "Cet email est déjà utilisé";
		}
		else {
			
			try {
				$createAccount = $bdd->prepare("INSERT INTO infoperso(username, email, password) VALUES (?, ?, ?);");
			    // $result["password"] = $passwordHashed;
				$createAccount->execute(array($username, $email, $passwordHashed));
				$result["success"] = true;
			} catch (Exception $e) {
				$result["success"] = false;
				$result["error"] = "Une erreur est survenue lors de la création de votre compte: " . $e->getMessage();
			}
		}
	}
} else {
	$result["success"] = false;
	$result["error"] = "Veuillez remplir tous les champs";
}

echo json_encode($result);
*/
?>
