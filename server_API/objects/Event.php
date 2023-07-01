<?php
// Inclure votre fichier de configuration de base de données et la classe Event ici
include_once '../config/Database.php';
include_once '../objects/Event.php';
include_once '../actions/EventService.php';

$database = new Database();
$db = $database->getConnection();
$event = new Event($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    if ($_GET['action'] == 'read' && isset($_GET['userId'])) {
        $events = $event->read($_GET['userId']);
        
        // Convertir les résultats en un tableau associatif
        $eventsArray = [];
        while ($row = $events->fetch(PDO::FETCH_ASSOC)){
            $eventsArray[] = $row;
        }
        
        // Envoyer une réponse JSON
        header('Content-Type: application/json');
        echo json_encode($eventsArray);
    } 
} 
else if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    
}
// else {
//     // Vous pouvez renvoyer une erreur 405 (Méthode non autorisée) si la méthode de requête n'est pas supportée
//     http_response_code(405);
//     echo json_encode(["message" => "Method not allowed."]);
// }

class Event
{
	private $conn;

	public $name;
	public $date;
	public $description;

	public function __construct($db)
	{
		$this->conn = $db;
	}

	public function create($title, $description, $startDate, $endDate, $startTime, $endTime, $location, $userId) {
		$query = "INSERT INTO events SET Title=:Title, Description=:Description, StartDate=:StartDate, EndDate=:EndDate, StartTime=:StartTime, EndTime=:EndTime, Location=:Location, UserId=:UserId";

		$stmt = $this->conn->prepare($query);

		$stmt->bindParam(":Title", $title);
		$stmt->bindParam(":Description", $description);
		$stmt->bindParam(":StartDate", $startDate);
		$stmt->bindParam(":EndDate", $endDate);
		$stmt->bindParam(":StartTime", $startTime);
		$stmt->bindParam(":EndTime", $endTime);
		$stmt->bindParam(":Location", $location);
		$stmt->bindParam(":UserId", $userId);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}

	public function read($userId) {
		$query = "SELECT * FROM events WHERE UserId=:UserId";

		$stmt = $this->conn->prepare($query);
		$stmt->bindParam(":UserId", $userId);
		$stmt->execute();

		return $stmt;
	}

	public function update($id, $title, $description, $startDate, $endDate, $startTime, $endTime, $location, $userId) {
		$query = "UPDATE events SET Title=:Title, Description=:Description, StartDate=:StartDate, EndDate=:EndDate, StartTime=:StartTime, EndTime=:EndTime, Location=:Location, UserId=:UserId WHERE id=:id";

		$stmt = $this->conn->prepare($query);

		$stmt->bindParam(":id", $id);
		$stmt->bindParam(":Title", $title);
		$stmt->bindParam(":Description", $description);
		$stmt->bindParam(":StartDate", $startDate);
		$stmt->bindParam(":EndDate", $endDate);
		$stmt->bindParam(":StartTime", $startTime);
		$stmt->bindParam(":EndTime", $endTime);
		$stmt->bindParam(":Location", $location);
		$stmt->bindParam(":UserId", $userId);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}

	public function delete($eventId) {
		$query = "DELETE FROM events WHERE id=:EventId";
		$stmt = $this->conn->prepare($query);
		$stmt->bindParam(":EventId", $eventId);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}
}
?>
