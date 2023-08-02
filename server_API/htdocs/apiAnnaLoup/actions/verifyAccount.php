<?php

header('Content-Type: application/json');
include_once '../config/Database.php';
include_once '../objects/User.php';

$database = new Database();
$db = $database->getConnection();

$json = json_decode(file_get_contents('php://input'), true);

if (isset($json["action"]) && $json["action"] === "verifEmail") {

    // Récupérer l'identifiant de l'utilisateur et la valeur de vérification d'email
    $email_verif = $json["email_verif"];
    $user_id = $json["user_id"];

    // Mise à jour de la colonne 'email_verif' de l'utilisateur spécifié
    $stmt = $db->prepare("UPDATE infoperso SET email_verif = ? WHERE User_id = ?");
    $stmt->execute(array($email_verif, $user_id));

    if ($stmt->rowCount() > 0) {
        // Les données ont été mises à jour avec succès
        $userInstance = new User($db);
        $user = $userInstance->getUser($user_id);

        $res = ["success" => true, "user" => $user];
    } else {
        // Il y a eu une erreur lors de la mise à jour des données
        $res = ["success" => false, "error" => "Une erreur est survenue lors de la mise à jour des données."];
    }

    echo json_encode($res);
} else if (isset($json["action"]) && $json["action"] === "modifAccount") {

    $id = $json["id"];
    $email = $json["email"];
    $lastName = $json["lastName"];
    $firstName = $json["firstName"];
    $phone = $json["phone"];
    $address = $json["address"];
    // $profilePhoto = $json["profilePhoto"];

    // Mise à jour des données de l'utilisateur
    $stmt = $db->prepare("UPDATE infoperso SET email = ?, Nom = ?, Prenom = ?, Telephone = ?, Adresse = ? WHERE User_id = ?");
    $stmt->execute(array($email, $lastName, $firstName, $phone, $address, $id));

    if ($stmt->rowCount() > 0) {
        // Les données ont été mises à jour avec succès
        $userInstance = new User($db);
        $user = $userInstance->getUser($id);
        $res = ["success" => true, "user" => $user];
    } else {
        // Il y a eu une erreur lors de la mise à jour des données
        $res = ["success" => false, "error" => "Une erreur est survenue lors de la mise à jour des données."];
    }

    echo json_encode($res);
} else {
    $res = ["success" => false, "error" => "Action non reconnue"];
    echo json_encode($res);
}
?>
