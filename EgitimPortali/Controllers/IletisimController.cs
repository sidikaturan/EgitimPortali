using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Iletisimler;
using EgitimPortali.Request.Iletisim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IletisimController : ControllerBase
    {
        private readonly IIletisimRepository _iletisimRepository;
        private readonly IMapper _mapper;

        public IletisimController(IIletisimRepository iletisimRepository, IMapper mapper)
        {
            _iletisimRepository = iletisimRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Iletisim>))]

        public IActionResult KonuListele()
        {
            var deger = _iletisimRepository.IletisimListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult IletisimEkle(IletisimPostRequest iletisimCreate)
        {
            if (iletisimCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Iletisim>(iletisimCreate);

            if (!_iletisimRepository.IletisimEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
        [HttpDelete("{iletisimId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult IletisimSil(int iletisimId)
        {
            if (!_iletisimRepository.IletisimKontrol(iletisimId))
            {
                return NotFound();
            }

            var categoryToDelete = _iletisimRepository.IletisimGetir(iletisimId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_iletisimRepository.IletisimSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{iletisimId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int iletisimId)
        {
            if (!_iletisimRepository.IletisimKontrol(iletisimId))
                return NotFound();
            var kategori = _mapper.Map<IletisimDto>(_iletisimRepository.IletisimGetir(iletisimId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
    }
}
