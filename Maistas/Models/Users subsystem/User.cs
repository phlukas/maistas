using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class User
{
   

    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string HelpQuestion { get; set; }
    public string CardInfo { get; set; }
    public string Address { get; set; }

    public User()
    {

    }

    public User(int id, string name, string surname, string email, string username, string password, string role, string helpQuestion, string cardInfo, string address)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        HelpQuestion = helpQuestion;
        CardInfo = cardInfo;
        Address = address;
    }
}