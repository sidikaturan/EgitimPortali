using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Konu;
using EgitimPortali.Request.Konular;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KonularController : ControllerBase
    {
        private readonly IKonularRepository _konularRepository;
        private readonly IMapper _mapper;

        public KonularController(IKonularRepository konularRepository, IMapper mapper)
        {
            _konularRepository = konularRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Konular>))]
        public IActionResult KonuListele()
        {
            var deger = _konularRepository.KonulariListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpGet("konular/{dersid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Konular>))]
        public IActionResult KonuListele(int dersid)
        {
            var deger = _konularRepository.KonulariListele(dersid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpGet("Konuid")]
        public IActionResult KonuAdi(int id)
        {
            var deger = _konularRepository.KonuName(id);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult KonuEkle(KonularPostRequest konuCreate)
        {
            if (konuCreate == null)
                return BadRequest(ModelState);

            var category = _konularRepository.KonulariListele()
                .Where(x => x.Name.Trim().ToUpper() == konuCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Konular>(konuCreate);

            if (!_konularRepository.KonuEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{konuId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuGuncelle(int konuId, [FromBody] KonularUpdateRequest updatedKonu)
        {
            if (_konularRepository.KonuGuncelle(konuId, updatedKonu))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{konuId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int konuId)
        {
            if (!_konularRepository.KonuKontrol(konuId))
            {
                return NotFound();
            }

            var categoryToDelete = _konularRepository.KonuGetir(konuId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_konularRepository.KonuSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{konuId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int konuId)
        {
            if (!_konularRepository.KonuKontrol(konuId))
                return NotFound();
            var kategori = _mapper.Map<KonularDto>(_konularRepository.KonuGetir(konuId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
