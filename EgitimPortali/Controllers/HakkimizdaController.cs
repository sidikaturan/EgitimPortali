using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Hakkımızda;
using EgitimPortali.Request.Hakkimizda;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HakkimizdaController : ControllerBase
    {

        private readonly IHakkimizdaRepository _hakkimizdaRepository;
        private readonly IMapper _mapper;

        public HakkimizdaController(IHakkimizdaRepository hakkimizdaRepository, IMapper mapper)
        {
            _hakkimizdaRepository = hakkimizdaRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hakkimizda>))]

        public IActionResult KonuListele()
        {
            var deger = _hakkimizdaRepository.HakkimizdaListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult KonuEkle(HakkimizdaDto hakkimizdaCreate)
        {
            if (hakkimizdaCreate == null)
                return BadRequest(ModelState);



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Hakkimizda>(hakkimizdaCreate);

            if (!_hakkimizdaRepository.HakkimizdaEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{hakkimizid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuGuncelle(int hakkimizid, [FromBody] HakkimizdaUpdateRequest updatedAnasayfa)
        {
            if (_hakkimizdaRepository.HakkimizdaGuncelle(hakkimizid, updatedAnasayfa))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{hakkimizid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int hakkimizid)
        {
            if (!_hakkimizdaRepository.HakkimizdaKontrol(hakkimizid))
            {
                return NotFound();
            }

            var categoryToDelete = _hakkimizdaRepository.HakkimizdaGetir(hakkimizid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hakkimizdaRepository.HakkimizdaSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{hakkimizid}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int hakkimizid)
        {
            if (!_hakkimizdaRepository.HakkimizdaKontrol(hakkimizid))
                return NotFound();
            var kategori = _mapper.Map<HakkimizdaDto>(_hakkimizdaRepository.HakkimizdaGetir(hakkimizid));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}

