namespace PixelDataApp.Model
{
    public class UserForAdmin
    {
        public UserForAdmin(string Firstname, string LastName, string Username, string Password,
            DateTime DateOfBirth, string Email, string Role) 
        {
            this.FirstName = Firstname;
            this.LastName = LastName;
            this.Username = Username;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Role = Role;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
