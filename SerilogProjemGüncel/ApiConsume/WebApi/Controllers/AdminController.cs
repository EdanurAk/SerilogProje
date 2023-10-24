using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly Context _context;

        public AdminController(ILogger<EmployeeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public class MyTestClass
        {
            public int MyInt { get; set; }
            public string MyString { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Admin admin)
        {
            var user = await _context.Admins.FirstOrDefaultAsync(u => u.UserName == admin.UserName && u.Password == admin.Password);

            if (user != null)
            {
                // Kullanıcı doğrulandı
                _logger.LogInformation("Giriş Başarılı {@user} kişisi sisteme giriş yaptı. {@testVeri}", user, new MyTestClass { MyInt = 255, MyString = "Eda" });
                return Ok("Giriş başarılı.");
            }
            _logger.LogError("Kullanıcı adı veya şifre yanlış {@user} bilgileri hatalı", user);
            return Unauthorized("Kullanıcı adı veya şifre yanlış.");
        }
    }
}
