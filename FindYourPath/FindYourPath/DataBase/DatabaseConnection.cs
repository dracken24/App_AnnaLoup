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

/*
C'est une très bonne idée et une pratique courante dans le développement d'applications.
Lorsqu'un utilisateur désinstalle une application, toutes les données stockées localement
sur l'appareil, y compris les bases de données SQLite, sont généralement supprimées.
En sauvegardant les données utilisateur sur un serveur, vous pouvez vous assurer que ces
données ne seront pas perdues si l'utilisateur désinstalle l'application.

Pour réaliser cela, vous devez généralement mettre en place un service Web (API) qui sert
d'intermédiaire entre votre application mobile et votre base de données MySQL. Ce service
Web peut être écrit dans un langage comme PHP, Python, Node.js ou ASP.NET.

Ensuite, vous pouvez utiliser HttpClient dans votre application Xamarin pour envoyer des
requêtes HTTP à ce service Web. Le service Web recevrait ces requêtes, interagirait avec la
base de données MySQL pour lire, insérer, mettre à jour ou supprimer des données, et
renverrait une réponse à votre application.

Par exemple, pour ajouter un nouvel utilisateur, vous pourriez envoyer une requête POST à
une URL comme https://votresite.com/api/users avec le nom d'utilisateur, l'e-mail et le
mot de passe en tant que données de la requête. Le service Web interagirait ensuite avec
la base de données MySQL pour insérer le nouvel utilisateur et renverrait une réponse
indiquant si l'opération a réussi ou non.

Notez que la sécurité est un aspect important à prendre en compte lors de la création de
ce type de service Web. Il est important de s'assurer que les données sont transmises de
manière sécurisée, par exemple en utilisant HTTPS et en veillant à ce que les mots de passe
soient correctement hachés et salés avant d'être stockés dans la base de données.

Enfin, si vous préférez éviter la complexité de la création et de la maintenance d'un service
Web, il existe également des services backend comme Firebase qui offrent une base de données
en ligne facile à utiliser avec une API prête à l'emploi que vous pouvez utiliser pour stocker
et récupérer des données utilisateur. 
 */
