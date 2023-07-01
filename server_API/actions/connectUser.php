<?php

header('Content-Type: application/json');
include_once '../config/Database.php';
include_once '../objects/User.php';
$json = json_decode(file_get_contents('php://input'), true);

$database = new Database();
$db = $database->getConnection();

if (isset($json["username"]) and isset($json["password"])) {
	$username = htmlspecialchars($json["username"]);
	$password = htmlspecialchars($json["password"]);

	$getUser = $db->prepare("SELECT password FROM infoperso WHERE username = ?");
	$getUser->execute(array($username));

	// Si l'utilisateur existe
	if ($getUser->rowCount() > 0) {
		$user = $getUser->fetch();

		// Si le mot de passe est correct
		if (password_verify($password, $user["password"])) {
			$userInstance = new User($db); // Création d'une instance de la classe User
			$result["success"] = true;
			$result["user"] = $userInstance->getUser($username); // Appeler la méthode getUser sur l'instance de la classe User
		} else {
			$result["success"] = false;
			$result["error"] = "Le mot de passe est incorrect";
		}
	} else {
		$result["success"] = false;
		$result["error"] = "L'utilisateur n'existe pas";
	}
}else {
	$result["success"] = false;
	$result["error"] = "Veuillez remplir tous les champs";
}

echo json_encode($result);
?>
