using SQLite;
public class User
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Sexe { get; set; }
	public string Age { get; set; }
	public string Username { get; set; }
	public string Adress { get; set; }
	public string PhonePrimary { get; set; }
	public string PhoneOther { get; set; }
	public string Email { get; set; }
	public string ProfilePicture { get; set; }
}

