using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.DersIcerik;
using EgitimPortali.Request.DersIcerikleri;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DersIcerikleriController : ControllerBase
    {
        private readonly IDersIcerikRepository _dersicerikleriRepository;
        private readonly IMapper _mapper;

        public DersIcerikleriController(IDersIcerikRepository dersicerikleriRepository, IMapper mapper)
        {
            _dersicerikleriRepository = dersicerikleriRepository;
            _mapper = mapper;
        }
        [HttpGet("konular/{icerikid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Konular>))]
        public IActionResult DersIcerikleriniListele(int icerikid)
        {
            var deger = _dersicerikleriRepository.DersIcerikleriniListele(icerikid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpGet("son3ders/{icerikid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Konular>))]
        public IActionResult Son3Ders(int icerikid)
        {
            var deger = _dersicerikleriRepository.Son3Ders(icerikid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DersIcerikleri>))]
        public IActionResult DersListele()
        {
            var deger = _dersicerikleriRepository.DersIcerikleriniListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult DersEkle(DersIcerikleriPostRequest dersicerikleriCreate)
        {
            if (dersicerikleriCreate == null)
                return BadRequest(ModelState);

         

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<DersIcerikleri>(dersicerikleriCreate);

            if (!_dersicerikleriRepository.DersIcerikleriEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{derslericerikleriId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DersGuncelle(int derslericerikleriId, [FromBody] DersIcerikleriDto updatedDersicerikleri)
        {
            if (_dersicerikleriRepository.DersIcerikleriGuncelle(derslericerikleriId, updatedDersicerikleri))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{derslericerikleriId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DersSil(int derslericerikleriId)
        {
            if (!_dersicerikleriRepository.DersIcerikleriKontrol(derslericerikleriId))
            {
                return NotFound();
            }

            var categoryToDelete = _dersicerikleriRepository.DersIcerikleriGetir(derslericerikleriId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_dersicerikleriRepository.DersIcerikleriSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
        [HttpGet("{derslericerikleriId}")]
        [ProducesResponseType(200, Type = typeof(DersIcerikleri))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int derslericerikleriId)
        {
            if (!_dersicerikleriRepository.DersIcerikleriKontrol(derslericerikleriId))
                return NotFound();
            var kategori = _mapper.Map<DersIcerikleriDto>(_dersicerikleriRepository.DersIcerikleriGetir(derslericerikleriId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
