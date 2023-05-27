using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Anasayfa;
using EgitimPortali.Request.AnaSayfa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnasayfaController : ControllerBase
    {

        private readonly IAnasayfaRepository _anasayfaRepository;
        private readonly IMapper _mapper;

        public AnasayfaController(IAnasayfaRepository anasayfaRepository, IMapper mapper)
        {
            _anasayfaRepository = anasayfaRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnaSayfa>))]

        public IActionResult KonuListele()
        {
            var deger = _anasayfaRepository.AnaSayfaListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
  
        [HttpPost]
        public IActionResult KonuEkle(AnasayfaDto anasayfaCreate)
        {
            if (anasayfaCreate == null)
                return BadRequest(ModelState);

          

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<AnaSayfa>(anasayfaCreate);

            if (!_anasayfaRepository.AnaSayfaEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{anasayfaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AnaSayfaGuncelle(int anasayfaId, [FromBody] AnaSayfaUpdateRequest updatedAnasayfa)
        {
            if (_anasayfaRepository.AnaSayfaGuncelle(anasayfaId, updatedAnasayfa))
            {
                return Ok();
            }
            return NotFound();     
        }
        [HttpDelete("{anasayfaId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int anasayfaId)
        {
            if (!_anasayfaRepository.AnaSayfaKontrol(anasayfaId))
            {
                return NotFound();
            }

            var categoryToDelete = _anasayfaRepository.AnaSayfaGetir(anasayfaId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_anasayfaRepository.AnaSayfaSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{anasayfaId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int anasayfaId)
        {
            if (!_anasayfaRepository.AnaSayfaKontrol(anasayfaId))
                return NotFound();
            var kategori = _mapper.Map<AnasayfaDto>(_anasayfaRepository.AnaSayfaGetir(anasayfaId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
