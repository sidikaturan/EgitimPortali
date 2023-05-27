using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.TestCevaplari;
using EgitimPortali.Repository.TestSorulari;
using EgitimPortali.Request.TestCevap;
using EgitimPortali.Request.TestSoru;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestCevapController : ControllerBase
    {
        private readonly ITestCevapRepository _testcevapRepository;
        private readonly IMapper _mapper;
        public TestCevapController(ITestCevapRepository testcevapRepository, IMapper mapper)
        {
            _testcevapRepository = testcevapRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestCevap>))]

        public IActionResult TestSorulariniListele()
        {
            var deger = _testcevapRepository.TestCevapListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [Authorize]
        [HttpGet("KullaniciyaGore")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestCevap>))]

        public IActionResult KullaniciTestSorulariniListele()
        {
            var deger = _testcevapRepository.KullaniciTestCevapListele();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [Authorize]
        [HttpGet("kullanici/{testcevapId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestCevap>))]

        public IActionResult KullaniciTestSorulariniIdListele(int testcevapId)
        {
            var deger = _testcevapRepository.KullaniciTestCevaplariIdListele(testcevapId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(deger);
        }
        [HttpPost]
        public IActionResult TestEkle(TestCevapPostRequest testCreate)
        {
            if (testCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testcevapRepository.TestCevapEkle(testCreate))
            {
                ModelState.AddModelError("", "Bir hata meydana geldi.");
                return StatusCode(500, ModelState);
            }

            return Ok("Başarıyla Eklendi.");
        }
 
        [HttpDelete("{testcevapId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult TestSil(int testcevapId)
        {
            if (!_testcevapRepository.TestCevapKontrol(testcevapId))
            {
                return NotFound("Böyle bir kayıt mevcut değildir.");
            }

            var categoryToDelete = _testcevapRepository.TestCevapGetir(testcevapId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testcevapRepository.TestCevapSil(categoryToDelete))
            {
                ModelState.AddModelError("", "Silme işlemi gerçekleştirilemedi.");
            }

            return NoContent();
        }

        [HttpGet("{testcevapId}")]
        [ProducesResponseType(200, Type = typeof(TestCevap))]
        [ProducesResponseType(400)]
        public IActionResult TestGetir(int testcevapId)
        {
            if (!_testcevapRepository.TestCevapKontrol(testcevapId))
                return NotFound("Böyle bir test bulunamadı");
            var kategori = _mapper.Map<TestCevapDto>(_testcevapRepository.TestCevapGetir(testcevapId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(kategori);
        }


    }
}
