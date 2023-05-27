using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.SoruCevap;
using EgitimPortali.Request.SorularinCevaplari;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorularinCevaplariController : ControllerBase
    {
        private readonly ISoruCevapRepository _soruCevapRepository;
        private readonly IMapper _mapper;

        public SorularinCevaplariController(ISoruCevapRepository soruCevapRepository, IMapper mapper)
        {
            _soruCevapRepository = soruCevapRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SorularinCevaplari>))]

        public IActionResult KonuListele()
        {
            var deger = _soruCevapRepository.SorularinCevaplariListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }

        [HttpPost]
        public IActionResult KonuEkle(SorularinCevaplariPostRequest sorucevapCreate)
        {
            if (sorucevapCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<SorularinCevaplari>(sorucevapCreate);

            if (!_soruCevapRepository.SorularinCevaplariEkle(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }

        [HttpPut("{sorucevapid}")]
        public IActionResult SoruCevapGuncelle(int sorucevapid)
        {
            if (_soruCevapRepository.SoruCevapGuncelle(sorucevapid))
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete("{sorucevapId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult KonuSil(int sorucevapId)
        {
            if (!_soruCevapRepository.SorularinCevaplariKontrol(sorucevapId))
            {
                return NotFound();
            }

            var categoryToDelete = _soruCevapRepository.SorularinCevaplariGetir(sorucevapId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_soruCevapRepository.SorularinCevaplariSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        [HttpGet("{sorucevapId}")]
        [ProducesResponseType(200, Type = typeof(Konular))]
        [ProducesResponseType(400)]
        public IActionResult KonuGetir(int sorucevapId)
        {
            if (!_soruCevapRepository.SorularinCevaplariKontrol(sorucevapId))
                return NotFound();
            var kategori = _mapper.Map<SoruCevapDto>(_soruCevapRepository.SorularinCevaplariGetir(sorucevapId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
        [HttpGet("cevaplar/{soruid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SorularinCevaplari>))]
        public IActionResult IcerikYorumlariniListele(int soruid)
        {
            var deger = _soruCevapRepository.CevaplariSoralaraGoreGetir(soruid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [Authorize]
        [HttpGet("kullanicicevaplar/{kullanicisoruid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SorularinCevaplari>))]
        public IActionResult KullaniciIcerikYorumlariniListele(int kullanicisoruid)
        {
            var deger = _soruCevapRepository.KullaniciCevaplariSorularaGoreGetir(kullanicisoruid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
    }
}
