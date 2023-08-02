<?php

header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

include_once '../config/Database.php';
include_once '../objects/Event.php';

$database = new Database();
$db = $database->getConnection();
$event = new Event($db);

$json = json_decode(file_get_contents('php://input'), true);

if (isset($json["action"])) {
	$action = $json["action"];
	switch ($action) {
		case "createCalendarEvent": // Creer un evenement dans le calendrier
			// TODO: Remove this line after debugging.
			var_dump(print_r($json, true));
			$title = isset($json["Title"]) ? $json["Title"] : '';
			$description = isset($json["Description"]) ? $json["Description"] : '';

			// Validate dates and times
			$startDateTimestamp = strtotime($json["StartDate"]);
			$endDateTimestamp = strtotime($json["EndDate"]);
			$startTimeTimestamp = strtotime($json["StartTime"]);
			$endTimeTimestamp = strtotime($json["EndTime"]);

			if ($startDateTimestamp === false || $endDateTimestamp === false || $startTimeTimestamp === false || $endTimeTimestamp === false) {
				http_response_code(400);
				echo json_encode(array(
					"message" => "Invalid date or time format.",
					"StartDate" => $json["StartDate"],
					"EndDate" => $json["EndDate"],
					"StartTime" => $json["StartTime"],
					"EndTime" => $json["EndTime"]
				));

				exit();  // Stop the script
			}

			// If dates and times are valid, convert them to desired format
			$startDate = isset($json["StartDate"]) ? date('Y-m-d', strtotime($json["StartDate"])) : '';
			$endDate = isset($json["EndDate"]) ? date('Y-m-d', strtotime($json["EndDate"])) : '';
			$startTime = isset($json["StartTime"]) ? date('H:i:s', strtotime($json["StartTime"])) : '';
			$endTime = isset($json["EndTime"]) ? date('H:i:s', strtotime($json["EndTime"])) : '';
			$location = isset($json["Location"]) ? $json["Location"] : '';
			$userId = isset($json["UserId"]) ? $json["UserId"] : '';

			var_dump($json);
			if ($event->create($title, $description, $startDate, $endDate, $startTime, $endTime, $location, $userId)) {
				http_response_code(201);
				echo json_encode(array("message" => "Event was created."));
			} else {
				http_response_code(503);
				echo json_encode(array("message" => "Unable to create event."));
			}
			break;
		case "read":
			// Si un UserId est fourni, récupérez uniquement les événements de cet utilisateur.
			// Sinon, récupérez tous les événements.
			$userId = isset($json["UserId"]) ? $json["UserId"] : null;

			$events = $event->read($userId);
			
			if ($events) {
				http_response_code(200);
				echo json_encode($events);
			} else {
				http_response_code(404);
				echo json_encode(array("message" => "No events found."));
			}
			break;

		case "updateCalendarEvent": // Mettre à jour un événement dans le calendrier
			// TODO: Remove this line after debugging.
			var_dump(print_r($json, true));
			$id = isset($json["id"]) ? $json["id"] : '';
			$title = isset($json["Title"]) ? $json["Title"] : '';
			$description = isset($json["Description"]) ? $json["Description"] : '';

			// Validate dates and times
			$startDateTimestamp = strtotime($json["StartDate"]);
			$endDateTimestamp = strtotime($json["EndDate"]);
			$startTimeTimestamp = strtotime($json["StartTime"]);
			$endTimeTimestamp = strtotime($json["EndTime"]);

			if ($startDateTimestamp === false || $endDateTimestamp === false || $startTimeTimestamp === false || $endTimeTimestamp === false) {
				http_response_code(400);
				echo json_encode(array(
					"message" => "Invalid date or time format.",
					"StartDate" => $json["StartDate"],
					"EndDate" => $json["EndDate"],
					"StartTime" => $json["StartTime"],
					"EndTime" => $json["EndTime"]
				));

				exit();  // Stop the script
			}

			// If dates and times are valid, convert them to desired format
			$startDate = isset($json["StartDate"]) ? date('Y-m-d', strtotime($json["StartDate"])) : '';
			$endDate = isset($json["EndDate"]) ? date('Y-m-d', strtotime($json["EndDate"])) : '';
			$startTime = isset($json["StartTime"]) ? date('H:i:s', strtotime($json["StartTime"])) : '';
			$endTime = isset($json["EndTime"]) ? date('H:i:s', strtotime($json["EndTime"])) : '';
			$location = isset($json["Location"]) ? $json["Location"] : '';
			$userId = isset($json["UserId"]) ? $json["UserId"] : '';

			var_dump($json);
			if ($event->update($id, $title, $description, $startDate, $endDate, $startTime, $endTime, $location, $userId)) {
				http_response_code(200);
				echo json_encode(array("message" => "Event was updated."));
			} else {
				http_response_code(503);
				echo json_encode(array("message" => "Unable to update event."));
			}
			break;
		case "delete":
			if (isset($json["id"])) {
				$eventId = $json["id"];
				if ($event->delete($eventId)) {
					http_response_code(200);
					echo json_encode(array("message" => "Event was deleted."));
				} else {
					http_response_code(503);
					echo json_encode(array("message" => "Unable to delete event."));
				}
			} else {
				http_response_code(400);
				echo json_encode(array("message" => "Event id is missing."));
			}
			break;
	}
}
// else {
// 	http_response_code(400);
// 	echo json_encode(array("message" => "No action received."));
// }

?>
