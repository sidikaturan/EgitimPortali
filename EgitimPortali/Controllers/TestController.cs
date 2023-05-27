using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Testler;
using EgitimPortali.Request.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestController(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Test>))]

        public IActionResult TestleriListele()
        {
            var deger = _testRepository.TestListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpGet("CozumKontrol/{cozumtestid}")]
        public IActionResult KullaniciTestKontrolu(int cozumtestid)
        {
            if (_testRepository.KullaniciCozumKontrol(cozumtestid))
                return BadRequest("Bu Testi Daha Önceden Çözmüşsünüz.");
            else
                return Ok();
        }

        [HttpPost]
        public IActionResult TestEkle(TestPostRequest testCreate)
        {
            if (testCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testRepository.TestEkle(testCreate))
            {
                ModelState.AddModelError("", "Bir hata meydana geldi.");
                return StatusCode(500, ModelState);
            }

            return Ok("Başarıyla Eklendi.");
        }
        [HttpPut("{testId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult TestGuncelle(int testId, [FromBody] TestUpdateRequest updatedTest)
        {
            if (_testRepository.TestGuncelle(testId, updatedTest))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{testId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult TestSil(int testId)
        {
            if (!_testRepository.TestKontrol(testId))
            {
                return NotFound("Böyle bir kayıt mevcut değildir.");
            }

            var categoryToDelete = _testRepository.TestGetir(testId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testRepository.TestSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Silme işlemi gerçekleştirilemedi.");
            }

            return NoContent();
        }

        [HttpGet("{testId}")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        public IActionResult TestGetir(int testId)
        {
            if (!_testRepository.TestKontrol(testId))
                return NotFound("Böyle bir test bulunamadı");
            var kategori = _mapper.Map<TestDto>(_testRepository.TestGetir(testId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }
        [AllowAnonymous]
        [HttpGet("konu/{konuid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Test>))]
        public IActionResult KonuListele(int konuid)
        {
            var deger = _testRepository.KonuyaGoreTestListele(konuid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
    }
}