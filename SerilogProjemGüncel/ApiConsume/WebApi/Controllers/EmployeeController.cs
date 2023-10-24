using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;


        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult EmployeeList()
        {
            var employee = _employeeService.TGetList();
            //_logger.LogInformation("Veriler Listelendi. {@employee}", employee);
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (employee.FirstName == null || employee.LastName == null || employee.Title == null || employee.Age == null)//bunları fluentvalidation ile yapacağım daha sonra
            {
                _logger.LogError("Geçersiz data girildi");
                return NotFound("Lütfen Ad,Soyad alanını geçmeyiniz");
            }
            _employeeService.TInsert(employee);

            _logger.LogInformation("Veriler Eklendi{@employee}", employee);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employeeService.TGetByID(id);
            if (employee == null)
            {
                _logger.LogError("silinecek personel id si bulunamadı");
                return NotFound("Bu id ye sahip personel bulunamamaktadır.");
            }
            _employeeService.TDelete(employee);
            _logger.LogInformation("{@employee} personel başarıyla silindi", employee);

            return Ok("Person ID" + " " + id + " " + "Başarıyla silindi.");
        }
        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            if (employee.FirstName == null || employee.LastName == null || employee.Title == null || employee.Age == null)
            {
                _logger.LogError("Değerler boş geçilemez");
                return NotFound("Lütfen Ad,Soyad alanını geçmeyiniz");
            }
            _employeeService.TUpdate(employee);
            _logger.LogInformation("Veriler Güncellendi{@employee}", employee);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeService.TGetByID(id);
            return Ok(employee);
        }
    }
}
