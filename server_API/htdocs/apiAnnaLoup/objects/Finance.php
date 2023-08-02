<?php

include_once '../config/Database.php';

$database = new Database();
$db = $database->getConnection();
$ressources = new Finance($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
	if ($_GET['action'] == 'read' && isset($_GET['UserId'])) {
		$ressource = $ressources->read($_GET['UserId']);

		// Convertir les résultats en un tableau associatif
		$eventsArray = [];
		while ($row = $ressource->fetch(PDO::FETCH_ASSOC)) {
			$ressourcesArray[] = $row;
		}

		// Envoyer une réponse JSON
		header('Content-Type: application/json');
		echo json_encode($ressourcesArray);
	}
} 

class Finance
{
	private $conn;

	public $UserId;
	public $Montant_Achat;
	public $Date_Achat;
	public $Description;

	public function __construct($db)
	{
		$this->conn = $db;
	}

	public function create($UserId, $Montant_Achat, $Date_Achat, $Description, $Type) {
		$query = "INSERT INTO Finances SET Description=:Description, Date_Achat=:Date_Achat, Montant_Achat=:Montant_Achat, Type=:Type, UserId=:UserId";

		$stmt = $this->conn->prepare($query);

		$stmt->bindParam(":Montant_Achat", $Montant_Achat);
		$stmt->bindParam(":Date_Achat", $Date_Achat);
		$stmt->bindParam(":Description", $Description);
		$stmt->bindParam(":Type", $Type);
		$stmt->bindParam(":UserId", $UserId);

		if ($stmt->execute()) {
			return ["success" => true];
		}

		return ["success" => false, "error" => "Une erreur est survenue lors de la création de votre compte: "];
	}

	public function read($userId) {
		$query = "SELECT * FROM Finances WHERE UserId=:UserId";

		$stmt = $this->conn->prepare($query);
		$stmt->bindParam(":UserId", $userId);
		$stmt->execute();

		return $stmt;
	}

	public function update($UserId, $Montant_Achat, $Date_Achat, $Description, $Type, $id)
	{
		$query = "UPDATE Finances SET Description=:Description, Date_Achat=:Date_Achat, Montant_Achat=:Montant_Achat, Type=:Type, UserId=:UserId WHERE id=:id";

		$stmt = $this->conn->prepare($query);

		$stmt->bindParam(":id", $id);
		$stmt->bindParam(":Montant_Achat", $Montant_Achat);
		$stmt->bindParam(":Date_Achat", $Date_Achat);
		$stmt->bindParam(":Description", $Description);
		$stmt->bindParam(":Type", $Type);
		$stmt->bindParam(":UserId", $UserId);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}

	public function delete($id)
	{
		$query = "DELETE FROM Finances WHERE id=:id";

		$stmt = $this->conn->prepare($query);
		$stmt->bindParam(":id", $id);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}
}

?>
