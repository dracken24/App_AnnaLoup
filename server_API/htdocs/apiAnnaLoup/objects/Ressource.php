<?php

include_once '../config/Database.php';

$database = new Database();
$db = $database->getConnection();
$ressources = new Ressource($db);

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

class Ressource
{
	private $conn;

	public $UserId;
	public $Nom;
	public $Adresse;
	public $Telephone;
	public $URL;
	public $Type;
	public $Description;

	public function __construct($db)
	{
		$this->conn = $db;
	}

	public function create($UserId, $Nom, $Adresse, $Telephone, $URL, $Type, $Description) {
        $query = "INSERT INTO ressources_prive SET Nom=:Nom, Adresse=:Adresse, Telephone=:Telephone, URL=:URL, Type=:Type, Description=:Description, UserId=:UserId";

		$stmt = $this->conn->prepare($query);

        $stmt->bindParam(":Nom", $Nom);
        $stmt->bindParam(":Adresse", $Adresse);
        $stmt->bindParam(":Telephone", $Telephone);
        $stmt->bindParam(":URL", $URL);
        $stmt->bindParam(":Type", $Type);
        $stmt->bindParam(":Description", $Description);
        $stmt->bindParam(":UserId", $UserId);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}

	public function read($userId) {
        $query = "SELECT * FROM ressources_prive WHERE UserId=:UserId";

        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(":UserId", $userId);
        $stmt->execute();

        return $stmt;
	}

	public function update($id, $name, $adresse, $phone, $url, $type, $description, $userId)
    {
        $query = "UPDATE ressources_prive SET Nom=:Nom, Adresse=:Adresse, Telephone=:Telephone, URL=:URL, Type=:Type, Description=:Description, UserId=:UserId WHERE id=:id";

        $stmt = $this->conn->prepare($query);

        $stmt->bindParam(":id", $id);
        $stmt->bindParam(":Nom", $name);
        $stmt->bindParam(":Adresse", $adresse);
        $stmt->bindParam(":Telephone", $phone);
        $stmt->bindParam(":URL", $url);
        $stmt->bindParam(":Type", $type);
        $stmt->bindParam(":Description", $description);
        $stmt->bindParam(":UserId", $userId);

        if ($stmt->execute()) {
            return true;
        }

        return false;
    }

	public function delete($id)
	{
		$query = "DELETE FROM ressources_prive WHERE id=:id";

		$stmt = $this->conn->prepare($query);
		$stmt->bindParam(":id", $id);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}
}

?>
