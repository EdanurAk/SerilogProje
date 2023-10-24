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
    public class UrunController : ControllerBase
    {
        private readonly IUrunService _UrunService;
        private readonly ILogger _logger;

        public UrunController(ILogger<UrunController> logger, IUrunService UrunService)
        {
            _UrunService = UrunService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult UrunList()
        {
            var urun = _UrunService.TGetList();
            //_logger.LogInformation("Veriler Listelendi. {@urun}", urun);
            return Ok(urun);
        }
        [HttpPost]
        public IActionResult AddUrun(Urun urun)
        {
            if (urun.UrunAd == null || urun.Marka == null || urun.Stok == null)//bunları fluentvalidation ile yapacağım daha sonra
            {
                _logger.LogError("Geçersiz data girildi");
                return NotFound("Lütfen Ad,Marka,Stok alanını geçmeyiniz");
            }

            _UrunService.TInsert(urun);
            _logger.LogInformation("Veriler Eklendi{@urun}", urun);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUrun(int id)
        {
            var urun = _UrunService.TGetByID(id);
            if (urun == null)
            {
                _logger.LogError("silinecek ürün bulunamadı");
                return NotFound("Bu id ye sahip ürün bulunamamaktadır.");
            }

            _UrunService.TDelete(urun);

            _logger.LogInformation("{@urun} ürün başarıyla silindi", urun);
            return Ok("Person ID" + " " + id + " " + "Başarıyla silindi.");
        }
        [HttpPut]
        public IActionResult UpdateUrun(Urun urun)
        {
            if (urun.UrunAd == null || urun.Marka == null || urun.Stok == null)
            {
                _logger.LogError("değerler boş geçilemez{@urun}",urun);
                return NotFound("Lütfen Ad,Marka,Stok alanını geçmeyiniz");
            }
            _UrunService.TUpdate(urun);
            _logger.LogInformation("Veriler Güncellendi{@urun}", urun);

            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetUrun(int id)
        {
            var urun = _UrunService.TGetByID(id);
            return Ok(urun);
        }
    }
}
