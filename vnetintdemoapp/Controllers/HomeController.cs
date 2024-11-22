using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using vnetintdemoapp.Models;

namespace vnetintdemoapp.Controllers
{
    public class HomeController(ILogger<HomeController> logger,
        IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public IActionResult Index()
        {
            var Courses = new List<Course>();
            string connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
            var sqlConnection = new MySqlConnection(connectionString);
            sqlConnection.Open();

            var sqlcommand = new MySqlCommand(
            "SELECT CourseID,CourseName,Rating FROM Course;", sqlConnection);
            using (MySqlDataReader sqlDatareader = sqlcommand.ExecuteReader())
            {
                while (sqlDatareader.Read())
                {
                    Courses.Add(new Course()
                    {
                        CourseID = int.Parse(sqlDatareader["CourseID"].ToString()),
                        CourseName = sqlDatareader["CourseName"].ToString(),
                        Rating = decimal.Parse(sqlDatareader["Rating"].ToString())
                    });
                }
            }
            return View(Courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
