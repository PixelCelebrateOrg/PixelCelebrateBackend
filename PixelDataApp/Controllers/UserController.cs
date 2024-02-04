using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixelDataApp.Database;
using PixelDataApp.Model;


namespace PixelDataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        PixelDataContext pixelDataContext = new PixelDataContext();

        [HttpPost]
        [Route("login")]
        public JsonResult Login([FromBody] UserLogin userLogin) 
        {
            var allUsers = pixelDataContext.Users;
            // Format string
            var existingUser = allUsers.SingleOrDefault(u => u.Username == userLogin.Username && u.Password == userLogin.Password);
            return new JsonResult(existingUser);
        }

        [HttpPost]
        [Route("GetDateAngajat")]
        public JsonResult GetUserByEmail([FromBody] String Email)
        {
            var allUsers = pixelDataContext.Users;
            // Format string
            var existingUser = allUsers.SingleOrDefault(u => u.Email == Email);
            return new JsonResult(existingUser);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public JsonResult GetAllUsers()
        {
            var allUsers = pixelDataContext.Users;
            // Format string
            var users = allUsers.FromSqlInterpolated($"SELECT * From Users");
            List<UserForAdmin> usersForAdmin= new List<UserForAdmin>();
            foreach ( var user in users)
            {
                usersForAdmin.Add(new UserForAdmin(user.FirstName, user.LastName, user.Username,
                    user.Password, user.DateOfBirth, user.Email, user.Role));
            }

            return new JsonResult(usersForAdmin);
        }

        [HttpGet]
        [Route("GetAllUsersAngajat")]
        public JsonResult GetAllUsersAngajat()
        {
            var allUsers = pixelDataContext.Users;
            // Format string
            var users = allUsers.FromSqlInterpolated($"SELECT * From Users");
            List<UserForAngajat> usersForAngajat = new List<UserForAngajat>();
            foreach (var user in users)
            {
                usersForAngajat.Add(new UserForAngajat(user.FirstName, user.LastName, user.Email));
            }

            return new JsonResult(usersForAngajat);
        }

        [HttpPost]
        [Route("CreateUser")]
        public JsonResult CreateUser([FromBody] UserToCreate userToCreate)
        {
            var allUsers = pixelDataContext.Users;

            //var existingUser = allUsers.SingleOrDefault(u => u.Email == userToCreate.Email || u.Username == userToCreate.Username);

            var existingUsers = allUsers.FromSqlInterpolated($"SELECT * From Users");
            foreach (var user in existingUsers)
            {
                if (String.Compare(user.Username, userToCreate.Username) == 0 || String.Compare(user.Email, userToCreate.Email) == 0)
                {
                    return new JsonResult(null);
                }
            }

            var random = new Random();
            var newID = random.Next();
            var userWithID = allUsers.FromSqlInterpolated($"SELECT * From Users Where UserId = {newID}");

            while (userWithID == null)
            {
                newID = random.Next();
                userWithID = allUsers.FromSqlInterpolated($"SELECT * From Users Where UserId = {newID}");
            }

            //var userInserted = allUsers.FromSqlInterpolated($"INSERT into Users(UserId, FirstName, LastName, Username, Password, DateOfBirth, Email, Role) Values({nrUsers},{userToCreate.FirstName},{userToCreate.LastName}, {userToCreate.Username}, {userToCreate.Password}, {userToCreate.DateOfBirth}, {userToCreate.Email}, {userToCreate.Role})");
            var newUser = new User(newID, userToCreate.FirstName, userToCreate.LastName, userToCreate.Username, userToCreate.Password, userToCreate.DateOfBirth, userToCreate.Email, userToCreate.Role);
            pixelDataContext.Users.Add(newUser);
            pixelDataContext.SaveChanges();

            return new JsonResult(newUser);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public JsonResult UpdateUser([FromBody] UserToCreate userToUpdate)
        {
            var allUsers = pixelDataContext.Users;

            //caut userul care vreau sa fie updatat dupa emailul primit
            var existingUser = allUsers.SingleOrDefault(u => u.Email == userToUpdate.Email);

            if(existingUser != null)
            {
                //daca exista, ii dau update cu noile valori
                if(userToUpdate.FirstName != "")
                {
                    existingUser.FirstName = userToUpdate.FirstName;
                }
                if(userToUpdate.LastName != "")
                {
                    existingUser.LastName = userToUpdate.LastName;
                }
                if(userToUpdate.Username != "")
                {
                    existingUser.Username = userToUpdate.Username;
                }
                if(userToUpdate.Password != "")
                {
                    existingUser.Password = userToUpdate.Password;
                }
                if (userToUpdate.DateOfBirth.CompareTo(new DateTime(1, 1, 1)) !=0)
                {
                    existingUser.DateOfBirth = userToUpdate.DateOfBirth;
                }
                if(userToUpdate.Role != "")
                {
                    existingUser.Role = userToUpdate.Role;
                }

                //pixelDataContext.SaveChanges();
            }
            pixelDataContext.SaveChanges();

            return new JsonResult(existingUser);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public JsonResult DeleteUser([FromBody] String Email)
        {
            try
            {
                pixelDataContext.Remove(pixelDataContext.Users.Single(u => u.Email == Email));

                pixelDataContext.SaveChanges();

                return new JsonResult("User Deleted");
            }
            catch(Exception ex)
            {
                return new JsonResult("User with email " + Email + " does not exist");
            }
        }

        [HttpPost]
        [Route("SetDay")]
        public JsonResult SetDay([FromBody] int numberOfDays)
        {
            Console.WriteLine(numberOfDays);

            //scris in fisier
            string fileName = "./" + "numberOfDays.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();

            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine(numberOfDays);
            sw.Close();


            //citit din fisier
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            string line;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                line = reader.ReadLine();
                reader.Close();
            }

            try
            {
                int noOfDays = Int32.Parse(line);
                Console.WriteLine(noOfDays);
            }
            catch(Exception ex)
            {
                int noOfDays = 7;
                Console.WriteLine(noOfDays);
            }

            return new JsonResult("Day was set");
        }

    }
}
