using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Soru;
using EgitimPortali.Request.Sorular;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorularController : ControllerBase
    {
        private readonly ISoruRepository _soruRepository;
        private readonly IMapper _mapper;

        public SorularController(ISoruRepository soruRepository, IMapper mapper)
        {
            _soruRepository = soruRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sorular>))]

        public IActionResult SoruListele()
        {
            var deger = _soruRepository.SorulariListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [Authorize]
        [HttpGet("KullaniciyaGore")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sorular>))]

        public IActionResult KullaniciyaGoreSoruListele()
        {
            var deger = _soruRepository.kullaniciyaGoreSorulariListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult SoruEkle(SorularPostRequest sorularCreate)
        {
            if (sorularCreate == null)
                return BadRequest(ModelState);



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Sorular>(sorularCreate);

            if (!_soruRepository.SoruEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }

        [HttpDelete("{soruId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int soruId)
        {
            if (!_soruRepository.SoruKontrol(soruId))
            {
                return NotFound();
            }

            var categoryToDelete = _soruRepository.SoruGetir(soruId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_soruRepository.SoruSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{soruId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult SoruGetir(int soruId)
        {
            if (!_soruRepository.SoruKontrol(soruId))
                return NotFound();
            var kategori = _mapper.Map<SorularDto>(_soruRepository.SoruGetir(soruId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
        [Authorize]
        [HttpGet("kullanici/{kullanicisoruId}")]

        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KullaniciSoruGetir(int kullanicisoruId)
        {
            if (!_soruRepository.SoruKontrol(kullanicisoruId))
                return NotFound();
            var kategori = _mapper.Map<SorularDto>(_soruRepository.KullaniciSoruGetir(kullanicisoruId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
        [HttpGet("dersleregore/{dersid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SorularinCevaplari>))]
        public IActionResult IcerikYorumlariniListele(int dersid)
        {
            var deger = _soruRepository.DerslereGoreSoruListeleme(dersid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
    }
}