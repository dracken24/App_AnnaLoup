<?php

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
        $query = "INSERT INTO ressources_prive SET Nom=:Nom, Adresse=:Adresse, Telephone=:Telephone, URL=:URL, Type=:Type, Description=:Description, User_Id=:User_Id";

		$stmt = $this->conn->prepare($query);

        $stmt->bindParam(":Nom", $Nom);
        $stmt->bindParam(":Adresse", $Adresse);
        $stmt->bindParam(":Telephone", $Telephone);
        $stmt->bindParam(":URL", $URL);
        $stmt->bindParam(":Type", $Type);
        $stmt->bindParam(":Description", $Description);
        $stmt->bindParam(":User_Id", $UserId);

		if ($stmt->execute()) {
			return true;
		}

		return false;
	}

	public function read($UserId) {
        $query = "SELECT * FROM ressources_prive WHERE User_Id=:UserId";

        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(":UserId", $UserId);
        $stmt->execute();

        $ressources = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $ressources;
	}

	// public function update($id, $UserId, $Nom, $Adresse, $Phone, $URL, $Type, $Description) {
	// 	$query = "UPDATE ressources_prive SET UserId=:UserId, Nom=:Nom, Adresse=:Adresse, Phone=:Phone, URL=:URL, Type=:Type, Description=:Description WHERE id=:id";

	// 	$stmt = $this->conn->prepare($query);

	// 	$stmt->bindParam(":id", $id);
	// 	$stmt->bindParam(":UserId", $UserId);
	// 	$stmt->bindParam(":Nom", $Nom);
	// 	$stmt->bindParam(":Adresse", $Adresse);
	// 	$stmt->bindParam(":Phone", $Phone);
	// 	$stmt->bindParam(":URL", $URL);
	// 	$stmt->bindParam(":Type", $Type);
	// 	$stmt->bindParam(":Description", $Description);

	// 	if ($stmt->execute()) {
	// 		return true;
	// 	}

	// 	return false;
	// }

	// public function delete($id) {
	// 	$query = "DELETE FROM ressources_prive WHERE id=:id";
	// 	$stmt = $this->conn->prepare($query);
	// 	$stmt->bindParam(":id", $id);

	// 	if ($stmt->execute()) {
	// 		return true;
	// 	}

	// 	return false;
	// }
}

?>
