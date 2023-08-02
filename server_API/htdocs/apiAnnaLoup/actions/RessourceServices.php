<?php

include_once '../objects/Ressource.php';
include_once '../config/Database.php';

header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");

$database = new Database();
$db = $database->getConnection();
$ressource = new Ressource($db);

$json = json_decode(file_get_contents('php://input'), true);

if (isset($json["action"])) {
	$action = $json["action"];
	switch ($action) {
		case "addPrivateRessource": // Ajouter une ressource à la base de données
			$UserId = isset($json["UserId"]) ? $json["UserId"] : '';
			$Nom = isset($json["Nom"]) ? $json["Nom"] : '';
			$Adresse = isset($json["Adresse"]) ? $json["Adresse"] : '';
			$Phone = isset($json["Phone"]) ? $json["Phone"] : '';
			$URL = isset($json["URL"]) ? $json["URL"] : '';
			$Type = isset($json["Type"]) ? $json["Type"] : '';
			$Description = isset($json["Description"]) ? $json["Description"] : '';

			if ($ressource->create($UserId, $Nom, $Adresse, $Phone, $URL, $Type, $Description)) {
				http_response_code(201);
				echo json_encode(array("message" => "Ressource was created."));
			} else {
				http_response_code(503);
				echo json_encode(array("message" => "Unable to create ressource."));
			}
			break;
        case "read":
            $userId = isset($json["UserId"]) ? $json["UserId"] : null;
            $ressources = $ressource->read($userId);

            if ($ressources) {
                http_response_code(200);
                echo json_encode($ressources);
            } else {
                http_response_code(404);
                echo json_encode(array("message" => "No ressources found."));
            }
            break;
		case "updateContact": // Mettre à jour un contact dans la base de données
			$id = isset($json["id"]) ? $json["id"] : '';
			$name = isset($json["name"]) ? $json["name"] : '';
			$adresse = isset($json["Adress"]) ? $json["Adress"] : '';
			$phone = isset($json["Phone"]) ? $json["Phone"] : '';
			$url = isset($json["Url"]) ? $json["Url"] : '';
			$type = isset($json["Type"]) ? $json["Type"] : '';
			$description = isset($json["Description"]) ? $json["Description"] : '';
			$userId = isset($json["UserId"]) ? $json["UserId"] : '';

			if ($ressource->update($id, $name, $adresse, $phone, $url, $type, $description, $userId)) {
				http_response_code(200);
				echo json_encode(array("message" => "Contact updated successfully"));
			} else {
				http_response_code(503);
				echo json_encode(array("message" => "Unable to update contact."));
			}
			break;
		case "delete": // Supprimer un contact de la base de données
			$id = isset($json["id"]) ? $json["id"] : '';
		
			if ($ressource->delete($id)) {
				http_response_code(200);
				echo json_encode(array("message" => "Contact deleted successfully"));
			} else {
				http_response_code(503);
				echo json_encode(array("message" => "Unable to delete contact."));
			}
			break;
	}
}
?>
