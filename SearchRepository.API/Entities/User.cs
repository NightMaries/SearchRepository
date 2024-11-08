namespace SearchRepository.API.Entities;

public class User
{   
    public int Id {get; set;}
    public string Login{get; set;}
    public string Token {get; set;}

    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}