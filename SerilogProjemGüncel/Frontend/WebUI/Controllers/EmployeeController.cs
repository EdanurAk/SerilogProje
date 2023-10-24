using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebUI.Models.Employee;

namespace WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:32978/api/Employee");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(jsonData);//json formatda gelen veriyi deserialize ettim
                return View(values);
            }

            return View();
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel addEmployeeViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(addEmployeeViewModel);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");//içeriğin dönüşümü için kullanılan bir sınıf
            var responseMessage = await client.PostAsync("http://localhost:32978/api/Employee", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:32978/api/Employee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:32978/api/Employee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateEmployeeViewModel>(jsonData);
                return View(values);
            }
            return View();
        }
    //    var client = _httpClientFactory.CreateClient();
    //    var responseMessage = await client.GetAsync($"http://localhost:32978/api/Urun/{id}");
    //            if (responseMessage.IsSuccessStatusCode)
    //            {
    //                var jsonData = await responseMessage.Content.ReadAsStringAsync();
    //    var values = JsonConvert.DeserializeObject<UpdateUrunViewModel>(jsonData);//json formatda gelen veriyi deserialize ettim
    //                return View(values);
    //}
    //            return View();
    [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");//içeriğin dönüşümü için kullanılan bir sınıf
            var responseMessage = await client.PutAsync("http://localhost:32978/api/Employee", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
