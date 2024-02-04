using System;
using System.Collections.Generic;

namespace PixelDataApp.Database
{
    public partial class User
    {
        public User()
        {
        }
        public User(int UserId, string Firstname, string LastName, string Username, string Password,
        DateTime DateOfBirth, string Email, string Role)
        {
            this.UserId = UserId;
            this.FirstName = Firstname;
            this.LastName = LastName;
            this.Username = Username;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Role = Role;
        }
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
