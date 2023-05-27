using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Kullanici;
using EgitimPortali.Request.Kullanicilar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IMapper _mapper;

        public KullaniciController(IKullaniciRepository kullaniciRepository, IMapper mapper)
        {
            _kullaniciRepository = kullaniciRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Kullanicilar>))]

        public IActionResult KullaniciListele()
        {
            var deger = _kullaniciRepository.KullanicilariListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }

        [HttpPost]
        public IActionResult KullaniciEkle(KullanicilarPostRequest kullaniciCreate)
        {
            if (kullaniciCreate == null)
                return BadRequest(ModelState);

            var category = _kullaniciRepository.KullanicilariListele()
                .Where(x => x.Mail.Trim().ToUpper() == kullaniciCreate.Mail.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Kullanicilar>(kullaniciCreate);

            if (!_kullaniciRepository.KullaniciEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{kullaniciId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KullaniciGuncelle(int kullaniciId, [FromBody] KullanicilarUpdateRequest updatedKullanici)
        {
            if (_kullaniciRepository.KullaniciGuncelle(kullaniciId, updatedKullanici))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{kullaniciId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KullaniciSil(int kullaniciId)
        {
            if (!_kullaniciRepository.KullaniciKontrol(kullaniciId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_kullaniciRepository.KullaniciSil(kullaniciId))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{kullaniciId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KullaniciGetir(int kullaniciId)
        {
            if (!_kullaniciRepository.KullaniciKontrol(kullaniciId))
                return NotFound();
            var kategori = _mapper.Map<KullaniciDto>(_kullaniciRepository.KullaniciGetir(kullaniciId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
        [Authorize]
        [HttpGet("oturum")]
        [ProducesResponseType(400)]
        public IActionResult OturumKullaniciGetir()
        {  
            var kategori = _kullaniciRepository.OturumGetir();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
