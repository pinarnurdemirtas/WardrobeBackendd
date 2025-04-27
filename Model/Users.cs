namespace WardrobeBackendd.Model;

public class Users
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string City { get; set; }
    public bool Is_verified { get; set; }
	public string Gender { get; set; } 

}