using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.TestSorulari;
using EgitimPortali.Request.TestSoru;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestSoruController : ControllerBase
    {
        private readonly ITestSoruRepository _testsoruRepository;
        private readonly IMapper _mapper;

        public TestSoruController(ITestSoruRepository testsoruRepository, IMapper mapper)
        {
            _testsoruRepository = testsoruRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestSoru>))]

        public IActionResult TestSorulariniListele()
        {
            var deger = _testsoruRepository.TestSoruListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }

        [HttpPost]
        public IActionResult TestEkle(TestSoruPostRequest testCreate)
        {
            if (testCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testsoruRepository.TestSoruEkle(testCreate))
            {
                ModelState.AddModelError("", "Bir hata meydana geldi.");
                return StatusCode(500, ModelState);
            }

            return Ok("Başarıyla Eklendi.");
        }
        [HttpPut("{testsoruId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult TestGuncelle(int testsoruId, [FromBody] TestSoruUpdateRequest updatedTest)
        {
            if (_testsoruRepository.TestSoruGuncelle(testsoruId, updatedTest))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{testsoruId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult TestSil(int testsoruId)
        {
            if (!_testsoruRepository.TestSoruKontrol(testsoruId))
            {
                return NotFound("Böyle bir kayıt mevcut değildir.");
            }

            var categoryToDelete = _testsoruRepository.TestSoruGetir(testsoruId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testsoruRepository.TestSoruSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Silme işlemi gerçekleştirilemedi.");
            }

            return NoContent();
        }

        [HttpGet("{testsoruId}")]
        [ProducesResponseType(200, Type = typeof(TestSoru))]
        [ProducesResponseType(400)]
        public IActionResult TestGetir(int testsoruId)
        {
            if (!_testsoruRepository.TestSoruKontrol(testsoruId))
                return NotFound("Böyle bir test bulunamadı");
            var kategori = _mapper.Map<TestSoruDto>(_testsoruRepository.TestSoruGetir(testsoruId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }

        [HttpGet("testsorulari/{testid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestSoru>))]
        public IActionResult KonuListele(int testid)
        {
            var deger = _testsoruRepository.TesteGoreSoruListele(testid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
    }
}
