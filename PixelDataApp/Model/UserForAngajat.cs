namespace PixelDataApp.Model
{
    public class UserForAngajat
    {
        public UserForAngajat(string Firstname, string LastName, string Email)
        {
            this.FirstName = Firstname;
            this.LastName = LastName;
            this.Email = Email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
