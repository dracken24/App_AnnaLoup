using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public class UserService
{
	readonly SQLiteAsyncConnection _database;
	private DatabaseConnection dbConn;

	public UserService(string dbPath)
	{
		Console.WriteLine("AAA");
		_database = new SQLiteAsyncConnection(dbPath);
		_database.CreateTableAsync<User>().Wait();

		dbConn = new DatabaseConnection();
	}

	public UserService()
	{
		Console.WriteLine("YYY");
		dbConn = new DatabaseConnection();
	}

	public Task<List<User>> GetUsersAsync()
	{
		return _database.Table<User>().ToListAsync();
	}

	public Task<User> GetUserAsync(int id)
	{
		return _database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
	}

	public Task<int> SaveUserAsync(User user)
	{
		if (user.Id != 0)
		{
			return _database.UpdateAsync(user);
		}
		else
		{
			return _database.InsertAsync(user);
		}
	}

	public Task<int> DeleteUserAsync(User user)
	{
		return _database.DeleteAsync(user);
	}

	public void GetUsers()
	{
		var connection = dbConn.OpenConnection();

		string query = "SELECT * FROM users";
		MySqlCommand cmd = new MySqlCommand(query, connection);

		// Execute the command and get the results
		MySqlDataReader dataReader = cmd.ExecuteReader();

		// Read the results
		while (dataReader.Read())
		{
			Console.WriteLine(dataReader["username"] + ": " + dataReader["email"]);
		}

		// Close the connection
		dbConn.CloseConnection();
	}

	// Add an user to database
	public void AddUser(string username, string email, string password)
	{
		Console.WriteLine("ADD USER");
		var connection = dbConn.OpenConnection();

		string query = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
		MySqlCommand cmd = new MySqlCommand(query, connection);

		// TODO: for debug, erase
		Console.WriteLine("User  : " + username);
		Console.WriteLine("Email : " + email);
		Console.WriteLine("Pass  : " + password);

		// Use parameters to prevent SQL injections
		cmd.Parameters.AddWithValue("@username", username);
		cmd.Parameters.AddWithValue("@email", email);
		cmd.Parameters.AddWithValue("@password", password); // consider hashing the password before storing

		cmd.ExecuteNonQuery();  // Execute the command

		dbConn.CloseConnection();  // Close the connection
	}
}
