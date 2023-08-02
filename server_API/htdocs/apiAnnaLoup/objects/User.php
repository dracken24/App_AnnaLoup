<?php

class User {
    private $db;

    public function __construct($db) {
        $this->db = $db;
    }

    public function create($username, $email, $password, $firstname, $lastname, $phone, $birthDate, $sexe, $address, $email_verification_token) {
        $passwordHashed = password_hash($password, PASSWORD_DEFAULT);
        
        $checkIfUserExist = $this->db->prepare("SELECT * FROM infoperso WHERE username = ?");
        $checkIfUserExist->execute(array($username));
        
        $checkIfEmailExist = $this->db->prepare("SELECT * FROM infoperso WHERE email = ?");
        $checkIfEmailExist->execute(array($email));
        
        if ($checkIfUserExist->rowCount() > 0) {
            return ["success" => false, "error" => "Ce nom d'utilisateur est déjà utilisé"];
        } else if ($checkIfEmailExist->rowCount() > 0) {
            return ["success" => false, "error" => "Cet email est déjà utilisé"];
        }
        
        try {
            $createAccount = $this->db->prepare("INSERT INTO infoperso(username, Nom, Prenom, Telephone, Date_Naissance, Sexe, Adresse, email, email_verification_token, password) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?);");
            $createAccount->execute(array($username, $lastname, $firstname, $phone, $birthDate, $sexe, $address, $email, $email_verification_token, $passwordHashed));
            return ["success" => true];
        } catch (Exception $e) {
            return ["success" => false, "error" => "Une erreur est survenue lors de la création de votre compte: " . $e->getMessage()];
        }
    }
    public function getUser($username) {
        $getUser = $this->db->prepare("SELECT * FROM infoperso WHERE username = ?");
        $getUser->execute(array($username));
        
        if ($getUser->rowCount() > 0) {
            return ["success" => true, "user" => $getUser->fetch()];
        } else {
            return ["success" => false, "error" => "L'utilisateur n'existe pas"];
        }
    }
}

?>
