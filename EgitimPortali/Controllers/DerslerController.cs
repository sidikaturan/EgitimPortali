using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Ders;
using EgitimPortali.Request.Dersler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DerslerController : ControllerBase
    {
        private readonly IDerslerRepository _derslerRepository;
        private readonly IMapper _mapper;

        public DerslerController(IDerslerRepository derslerRepository, IMapper mapper)
        {
            _derslerRepository = derslerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dersler>))]
        public IActionResult DersListele()
        {
            var deger = _derslerRepository.DersleriListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult DersEkle(DerslerDto derslerCreate)
        {
            if (derslerCreate == null)
                return BadRequest(ModelState);

            var category = _derslerRepository.DersleriListele()
                .Where(x => x.Name.Trim().ToUpper() == derslerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Dersler>(derslerCreate);

            if (!_derslerRepository.DersEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{derslerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DersGuncelle(int derslerId, [FromBody] DerslerUpdateRequest updatedDersler)
        {
            if (_derslerRepository.DersGuncelle(derslerId, updatedDersler))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{derslerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DersSil(int derslerId)
        {
            if (!_derslerRepository.DersKontrol(derslerId))
            {
                return NotFound();
            }

            var categoryToDelete = _derslerRepository.DersGetir(derslerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_derslerRepository.DersSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("{derslerId}")]
        [ProducesResponseType(200, Type = typeof(Kategoriler))]
        [ProducesResponseType(400)]
        public IActionResult DersGetir(int derslerId)
        {
            if (!_derslerRepository.DersKontrol(derslerId))
                return NotFound();
            var kategori = _mapper.Map<DerslerDto>(_derslerRepository.DersGetir(derslerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
        [HttpGet("dersler/{derslerinKategorisiId}")]
        [ProducesResponseType(200, Type = typeof(Kategoriler))]
        [ProducesResponseType(400)]
        public IActionResult DersKategorisiGetir(int derslerinKategorisiId)
        {
            if (!_derslerRepository.DersKontrol(derslerinKategorisiId))
                return NotFound();
            var kategori = _derslerRepository.KategoriAdi(derslerinKategorisiId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}