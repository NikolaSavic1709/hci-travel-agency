using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public UserType Auth { get; set; }
    public virtual List<Arrangement> Arrangements { get; set; }


    public User(string name, string email, string password, string surname, string phoneNumber)
    {
        Name = name;
        Email = email;
        Password = password;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Auth = UserType.Client;
        Arrangements = new List<Arrangement>();
    }

    public User(int id, string name, string email, string password, string surname, string phoneNumber, UserType auth, List<Arrangement> arrangements)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Auth = auth;
        Arrangements = arrangements;
    }
}
