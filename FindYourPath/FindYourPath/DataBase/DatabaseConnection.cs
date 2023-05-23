using System;
using MySqlConnector;

public class DatabaseConnection
{
	private MySqlConnection connection;
	private string server;
	private string database;
	private string uid;
	private string password;

	// Constructor
	public DatabaseConnection(string connectionString)
	{
		connection = new MySqlConnection(connectionString);
	}

	public DatabaseConnection(string server, string database, string uid, string password)
	{
		string connectionString;
		connectionString = "SERVER=" + server + ";" + "DATABASE=" +
		database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

		connection = new MySqlConnection(connectionString);
	}

	public DatabaseConnection()
	{
		Console.WriteLine("22222222222222222222222222222222222222222222222222222222222222222222222222222222");
		server = "localhost";
		database = "my_database";
		uid = "Bob";
		password = "Serpents24.";

		string connectionString;
		connectionString = "SERVER=" + server + ";" + "DATABASE=" +
		database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

		connection = new MySqlConnection(connectionString);
	}

	public MySqlConnection OpenConnection()
	{
		if (connection.State == System.Data.ConnectionState.Closed)
			connection.Open();

		return connection;
	}

	public void CloseConnection()
	{
		if (connection.State == System.Data.ConnectionState.Open)
			connection.Close();
	}

	public bool VerifyUser(string username, string password)
{
	try
	{
		OpenConnection();

		string query = "SELECT COUNT(*) FROM Users WHERE username=@username AND password=@password"; // Remplacez "Users" par le nom réel de votre table, et "username" et "password" par les noms de vos colonnes

		using (MySqlCommand cmd = new MySqlCommand(query, connection))
		{
			cmd.Parameters.AddWithValue("@username", username);
			cmd.Parameters.AddWithValue("@password", password); // Assurez-vous que le mot de passe est correctement haché / crypté selon votre logique

			int count = Convert.ToInt32(cmd.ExecuteScalar());

			return (count > 0);
		}
	}
	catch (Exception ex)
	{
		// Log or handle the exception
		Console.WriteLine(ex.ToString());
		return false;
	}
	finally
	{
		CloseConnection();
	}
}

	// Execute a query
	public void ExecuteQuery(string query)
	{
		try
		{
			// Make sure the connection is open
			OpenConnection();

			// Create a command
			MySqlCommand command = new MySqlCommand(query, connection);

			// Execute the command
			command.ExecuteNonQuery();
		}
		finally
		{
			// Close the connection
			CloseConnection();
		}
	}
}
