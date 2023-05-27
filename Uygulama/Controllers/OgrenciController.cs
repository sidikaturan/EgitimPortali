using EgitimPortali.DTO;
using EgitimPortali.Request.DersTakipleri;
using EgitimPortali.Request.Konular;
using EgitimPortali.Request.Kullanicilar;
using EgitimPortali.Request.Sorular;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Uygulama.Models;

namespace Uygulama.Controllers
{
    [Authorize(Roles = "Öğrenci")]
    public class OgrenciController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Profilim()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Kullanici/oturum");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<KullanicilarUpdateRequest>(jsonString);
            if (values != null)
            {
                ViewBag.id = values.Id;
                return View(values);
            }
            else
                return RedirectToAction("GirisYap", "Login");
        }
        [HttpPost]
        public async Task<IActionResult> Profilim(KullanicilarUpdateRequest p)
        {

            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var jsonEmployee = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7179/api/Kullanici/" + p.Id, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Profilim");
            }
            return View(p);
        }
        public async Task<IActionResult> Testler()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/TestCevap/KullaniciyaGore");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<CozulenTestDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> SorulanSorular()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Sorular/KullaniciyaGore");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<SorularDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> SoruDetay(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            ViewBag.id = id;
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Sorular/kullanici/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<SorularDto>(jsonString);
            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/SorularinCevaplari/kullanicicevaplar/" + id);
            var jsonString2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<IEnumerable<SoruCevapDto>>(jsonString2);
            SoruDetaylari cs = new SoruDetaylari();
            cs.sorular = values;
            cs.sorucevap = values2;
            if (cs != null)
                return View(cs);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> YapilanYorumlar()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Yorumlar/KullaniciyaGore");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<YorumlarDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> YorumuOnayla(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var jsonEmployee = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7179/api/SorularinCevaplari/" + id, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("IzlenilenDersler");
            }
            return Json(id);
        }
        public async Task<IActionResult> YorumSil(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.DeleteAsync("https://localhost:7179/api/Yorumlar/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("YapilanYorumlar");
            }
            return View();
        }

        public async Task<IActionResult> DersTakip()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Dersler");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<DerslerDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        

        public async Task<IActionResult> IzlenilenDersler(int id)
        {
            OgrenciDersTakip cs = new OgrenciDersTakip();
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersTakip/KullaniciyaGore");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            cs.DersTakip = JsonConvert.DeserializeObject<List<DersTakipDto>>(jsonString);
            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Konular/konular/" + id);
            var jsonString2 = await responseMessage2.Content.ReadAsStringAsync();
            cs.Konular = JsonConvert.DeserializeObject<List<KonularDto>>(jsonString2);
            var responseMessage3 = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri");
            var jsonString3 = await responseMessage3.Content.ReadAsStringAsync();
            cs.DersIcerikleri = JsonConvert.DeserializeObject<List<DersIcerikleriDto>>(jsonString3);

            if (cs != null)
                return View(cs);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> IzlemeGuncelle(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var jsonEmployee = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7179/api/DersTakip/id?id=" + id , content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("IzlenilenDersler");
            }
            return Json(id);
        }
        public async Task<IActionResult> IzlemeEkle(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var jsonEmployee = JsonConvert.SerializeObject(id);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/DersTakip?id=" +id, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("IzlenilenDersler");
            }
            return RedirectToAction();
        }
        public async Task<IActionResult> Dersler()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Dersler");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<DerslerDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> YeniSoruSor(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> YeniSoruSor(SorularPostRequest p)
        {

            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Sorular", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SorulanSorular");
            }
            return View(p);
        }
        public async Task<IActionResult> TestCevaplarim(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/TestCevap/kullanici/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<TestCevapDto>>(jsonString);
            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/TestSoru/testsorulari/" + id);
            var jsonString2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<TestSoruDto>>(jsonString2);

            OgrenciCevap cs = new OgrenciCevap();
            cs.Test = values2;
            cs.TestCevap = values;
            if (cs != null)
                return View(cs);
            else
                return RedirectToAction("GirisYap", "Login");
        }
    }
}