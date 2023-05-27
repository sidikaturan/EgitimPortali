using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.DersTakipleri;
using EgitimPortali.Repository.Hakkımızda;
using EgitimPortali.Request.DersTakipleri;
using EgitimPortali.Request.Hakkimizda;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DersTakipController : ControllerBase
    {
        private readonly IDersTakipRepository _derstakipRepository;
        private readonly IMapper _mapper;
        public DersTakipController(IDersTakipRepository derstakipRepository, IMapper mapper)
        {
            _derstakipRepository = derstakipRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DersTakip>))]

        public IActionResult DersTakipListele()
        {
            var deger = _derstakipRepository.DersTakipListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpGet("KullaniciyaGore")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DersTakip>))]

        public IActionResult KullaniciDersTakipListele()
        {
            var deger = _derstakipRepository.KullaniciyaGoreDersTakipListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult DersTakipEkle(int id)
        {
            if (id == null)
                return BadRequest(ModelState);

        
            if (!_derstakipRepository.KullaniciDersTakipKontrol(id))
            {
                return Ok();
            }

            if (!_derstakipRepository.DersTakipEkle(id))
            {
                ModelState.AddModelError("", "Bir hata meydana geldi. Kaydetme işlemi başarısız oldu.");
                return StatusCode(500, ModelState);
            }

            return Ok("Başarıyla Eklendi");
        }
        [HttpPut("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuGuncelle(int id)
        {
            if (_derstakipRepository.DersTakipGuncelle(id))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{derstakipid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int derstakipid)
        {
            if (!_derstakipRepository.DersTakipKontrol(derstakipid))
            {
                return NotFound();
            }
            var categoryToDelete = _derstakipRepository.DersTakipGetir(derstakipid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_derstakipRepository.DersTakipSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return NoContent();
        }
        [HttpGet("{derstakipid}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int derstakipid)
        {
            if (!_derstakipRepository.DersTakipKontrol(derstakipid))
                return NotFound();
            var kategori = _mapper.Map<DersTakipDto>(_derstakipRepository.DersTakipGetir(derstakipid));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
