using PixelDataApp;
using PixelDataApp.Database;
using PixelDataApp.Model;
using PixelDataApp.Services;
using System.Threading;

public class BackgroundWorkerService : BackgroundService
{
    private readonly IMailService _mailService;
    readonly ILogger<BackgroundWorkerService> _logger;
    PixelDataContext pixelDataContext = new PixelDataContext();

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IMailService _MailService)
    {
        _logger = logger;
        _mailService = _MailService;
    }

    public async Task<bool> SendMailAsync(MailData mailData)
    {
        return await _mailService.SendMailAsync(mailData);
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            string fileName = "./" + "numberOfDays.txt";
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            string line;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                line = reader.ReadLine();
                reader.Close();
            }
            int noOfDays;
            try
            {
                noOfDays = Int32.Parse(line);
            }
            catch (Exception ex)
            {
                noOfDays = 7;
            }

            DateTime DateToCheck = DateTime.Now.AddDays(noOfDays);

            var allUsers = pixelDataContext.Users;
            List<User> usersWithBirthday = new List<User>();
            foreach (var user in allUsers)
            {
                //verific daca sunt useri care au birthday data de mai sus
                if(user.DateOfBirth.Day == DateToCheck.Day && user.DateOfBirth.Month == DateToCheck.Month)
                {
                    usersWithBirthday.Add(user);
                }
            }

            foreach (var user in allUsers)
            {
                if(!usersWithBirthday.Any(item => item.Email == user.Email))
                {
                    //Console.WriteLine("Mesaj pentru " + user.Email + ": " + "Userul " + )
                    foreach(var userBirthDay in usersWithBirthday)
                    {
                        //Console.WriteLine("Mesaj pentru " + user.Email + ": " + "Userul " + userBirthDay.FirstName +
                        //    " " + userBirthDay.LastName + " are ziua de nastere peste " + noOfDays + " zile");

                        MailData mailData = new MailData();
                        mailData.EmailToId = user.Email;
                        mailData.EmailToName = user.Username;
                        mailData.EmailBody = "Buna ziua, " + user.FirstName + " " + user.LastName + ". Userul " + userBirthDay.FirstName +
                            " " + userBirthDay.LastName + " are ziua de nastere peste " + noOfDays + " zile, in data de "
                            + DateToCheck;
                        mailData.EmailSubject = "BirthDay Pixel Celebrate";


                        //_mailService.SendMail(mailData);
                        //return await _mailService.SendMailAsync(mailData);
                        SendMailAsync(mailData);
                    }
                }
            }

            //pentru o zi ar fi 86400 * 1000
            await Task.Delay(120000, stoppingToken);
        }
    }
}