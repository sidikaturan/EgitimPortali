using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Rol;
using EgitimPortali.Request.Roller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;

        public RolController(IRolRepository rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Roller>))]

        public IActionResult KonuListele()
        {
            var deger = _rolRepository.RolleriListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult KonuEkle(RollerPostRequest konuCreate)
        {
            if (konuCreate == null)
                return BadRequest(ModelState);

            var category = _rolRepository.RolleriListele()
                .Where(x => x.Name.Trim().ToUpper() == konuCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Roller>(konuCreate);

            if (!_rolRepository.RolEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{rolId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuGuncelle(int rolId, [FromBody] RollerUpdateRequest updatedKonu)
        {
            if (_rolRepository.RolGuncelle(rolId, updatedKonu))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{rolId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int rolId)
        {
            if (!_rolRepository.RolKontrol(rolId))
            {
                return NotFound();
            }

            var categoryToDelete = _rolRepository.RolGetir(rolId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_rolRepository.RolSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{rolId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int rolId)
        {
            if (!_rolRepository.RolKontrol(rolId))
                return NotFound();
            var kategori = _mapper.Map<RolDto>(_rolRepository.RolGetir(rolId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}

