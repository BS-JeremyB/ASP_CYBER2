using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PresentationSiteWeb.Models;
using ToolsBoxADO;

namespace PresentationSiteWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _connectionString = @"Server=(localdb)\BStormLocalDB;Database=CYBER2_ASP;Trusted_Connection=True;TrustServerCertificate=true";

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public string Login(UserLoginForm login)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                 User? user = connection.ExecuteReader<User>("LoginUser",
                    champs => new User
                    {
                        Id = (int)champs["Id"],
                        Pseudo = (string)champs["Pseudo"],
                        Email = (string)champs["Email"],
                    }, true, login).SingleOrDefault();

                return $"{user.Pseudo} est connecté !";


            }
        }

        [HttpGet]
        public IActionResult  Register() 
        { 
            return View();
        }

        
        [HttpPost]
        public IActionResult Register(UserCreateForm user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            { 
                connection.Open();
                connection.ExecuteNonQuery("CreateUser", true, new { user.Pseudo, user.Email, user.Password });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
