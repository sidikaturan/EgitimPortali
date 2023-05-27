using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Reklam;
using EgitimPortali.Request.Reklamlar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReklamlarController : ControllerBase
    {
        private readonly IReklamRepository _reklamRepository;
        private readonly IMapper _mapper;

        public ReklamlarController(IReklamRepository reklamRepository, IMapper mapper)
        {
            _reklamRepository = reklamRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reklamlar>))]

        public IActionResult ReklamListele()
        {
            var deger = _reklamRepository.ReklamlariListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult ReklamEkle([FromBody] ReklamlarPostRequest reklamCreate)
        {
            if (reklamCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reklamRepository.ReklamEkle(reklamCreate))
            {
                ModelState.AddModelError("", "Bir hata meydana geldi");
                return StatusCode(500, ModelState);
            }

            return Ok("Başarıyla Kaydedildi");
        }
        [HttpPut("{reklamId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult ReklamGuncelle(int reklamId, [FromBody] ReklamlarUpdateRequest updatedReklam)
        {
            if (_reklamRepository.ReklamGuncelle(reklamId, updatedReklam))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{reklamId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult ReklamSil(int reklamId)
        {
            if (!_reklamRepository.ReklamKontrol(reklamId))
            {
                return NotFound();
            }

            var categoryToDelete = _reklamRepository.ReklamGetir(reklamId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reklamRepository.ReklamSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{reklamlarId}")]
        [ProducesResponseType(200, Type = typeof(Reklamlar))]
        [ProducesResponseType(400)]
        public IActionResult ReklamGetir(int reklamlarId)
        {
            if (!_reklamRepository.ReklamKontrol(reklamlarId))
                return NotFound();
            var kategori = _mapper.Map<ReklamlarDto>(_reklamRepository.ReklamGetir(reklamlarId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
