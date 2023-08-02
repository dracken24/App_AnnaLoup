<?php
class Database {
    private $host = "localhost";
    private $db_name = "users";
    private $username = "root";
    private $password = "Serpents24.";
    public $conn;

    // get the database connection
    public function getConnection(){
        $this->conn = null;
        try{
            $this->conn = new PDO("mysql:host=" . $this->host . ";dbname=" . $this->db_name, $this->username, $this->password);
            $this->conn->exec("set names utf8");
        }catch(PDOException $exception){
            echo "Connection error: " . $exception->getMessage();
        }

        return $this->conn;
    }
}

// header('conten-type: text/html; charset=utf-8;');

// TODO: Connexion à la base de données, à modifier selon votre base de données
// localhost si vous utilisez un serveur local ou l'adresse IP de votre serveur
//$bdd = new PDO('mysql:host=localhost;dbname=users;charset=utf8mb4', 'root', 'Serpents24.');
?>