namespace PixelDataApp.Model
{
    public class UserLogin
    {
        public UserLogin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
