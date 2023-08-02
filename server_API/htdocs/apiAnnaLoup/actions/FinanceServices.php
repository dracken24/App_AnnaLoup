<?php

include_once '../objects/Finance.php';
include_once '../config/Database.php';

header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");

$database = new Database();
$db = $database->getConnection();
$ressource = new Finance($db);

$json = json_decode(file_get_contents('php://input'), true);

if (isset($json["action"])) {
	$action = $json["action"];
	switch ($action) {
		case "addTransaction": // Ajouter une ressource à la base de données
			$UserId = isset($json["UserId"]) ? $json["UserId"] : '';
			$Montant_Achat = isset($json["Montant_Achat"]) ? $json["Montant_Achat"] : '';
			$Date_Achat = isset($json["Date_Achat"]) ? $json["Date_Achat"] : '';
			$Description = isset($json["Description"]) ? $json["Description"] : '';
			$Type = isset($json["Type"])? $json["Type"] : '';
			
			$Date = date('Y-m-d H:i:s', strtotime($Date_Achat));

			if ($ressource->create($UserId, $Montant_Achat, $Date, $Description, $Type)) {
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
		case "updateTransaction": // Mettre à jour un contact dans la base de données
			$id = isset($json["id"]) ? $json["id"] : '';
			$UserId = isset($json["UserId"]) ? $json["UserId"] : '';
			$Montant_Achat = isset($json["Montant_Achat"]) ? $json["Montant_Achat"] : '';
			$Date_Achat = isset($json["Date_Achat"]) ? $json["Date_Achat"] : '';
			$Description = isset($json["Description"]) ? $json["Description"] : '';
			$Type = isset($json["Type"])? $json["Type"] : '';
			
			$Date = date('Y-m-d H:i:s', strtotime($Date_Achat));

			if ($ressource->update($UserId, $Montant_Achat, $Date, $Description, $Type, $id)) {
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
