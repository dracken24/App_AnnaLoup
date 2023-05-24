using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using FindYourPath.DataBase;

public class UserService
{
	readonly SQLiteAsyncConnection _database;

	public UserService(string dbPath)
	{
		_database = new SQLiteAsyncConnection(dbPath);
		_database.CreateTableAsync<User>().Wait();
	}

	// ... (autres méthodes) ...

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

	public async Task AddUser(string username, string email, string password)
	{
		var newUser = new User { Username = username, Email = email, Password = password };
		await SaveUserAsync(newUser);
	}

	public async Task<bool> VerifyUserAsync(string username, string password)
	{
		// Get the user with the given username
		var user = await _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();

		// If no such user exists, return false
		if (user == null)
		{
			return false;
		}

		// Compare the password with the one in the database
		// NOTE: This assumes that the password in the database is stored in plaintext. 
		// In a real application, you should NEVER store passwords in plaintext. 
		// You should store a hash of the password and compare that instead.
		if (user.Password == password)
		{
			return true;
		}

		// If the passwords do not match, return false
		return false;
	}

}
