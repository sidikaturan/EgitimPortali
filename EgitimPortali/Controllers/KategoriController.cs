using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Kategori;
using EgitimPortali.Request.Kategoriler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategoriController : ControllerBase
    {
        private readonly IKategorilerRepository _kategorilerRepository;
        private readonly IMapper _mapper;

        public KategoriController(IKategorilerRepository kategorilerRepository, IMapper mapper)
        {
            _kategorilerRepository = kategorilerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Kategoriler>))]

        public IActionResult KategoriListele()
        {
            var deger = _kategorilerRepository.KategorileriListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult KategoriEkle(KategoriDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _kategorilerRepository.KategorileriListele()
                .Where(x => x.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Kategoriler>(categoryCreate);

            if (!_kategorilerRepository.KategoriEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KategoriGuncelle(int categoryId, [FromBody] KategoriUpdateRequest updatedCategory)
        {
            if (_kategorilerRepository.KategoriGuncelle(categoryId, updatedCategory))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KategoriSil(int categoryId)
        {
            if (!_kategorilerRepository.KategoriKontrol(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = _kategorilerRepository.KategoriGetir(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_kategorilerRepository.KategoriSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    
        [HttpGet("{kategoriId}")]
        [ProducesResponseType(200, Type = typeof(Kategoriler))]
        [ProducesResponseType(400)]
        public IActionResult KategoriGetir(int kategoriId)
        {
            if (!_kategorilerRepository.KategoriKontrol(kategoriId))
                return NotFound();
            var kategori = _mapper.Map<KategoriDto>(_kategorilerRepository.KategoriGetir(kategoriId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}