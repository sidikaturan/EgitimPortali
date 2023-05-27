using EgitimPortali.DTO;
using EgitimPortali.Request.SorularinCevaplari;
using EgitimPortali.Request.TestCevap;
using EgitimPortali.Request.Yorumlar;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Uygulama.Models;

namespace Uygulama.Controllers
{
    public class MerkezeTegetController : Controller
    {
        public IActionResult Anasayfa()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Iletisim()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Iletisim(IletisimDto p)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Iletisim", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return View();
            }
            return View(p);
        }

        public async Task<IActionResult> Ders(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<DersIcerikleriDto>(jsonEmployee);
                ViewBag.id = id;
                ViewBag.konuid = values.KonularID;
                if (values != null)
                    return View(values);
                else
                    return RedirectToAction("GirisYap", "Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> YorumYap(YorumlarPostRequest p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Yorumlar", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return View();
            }
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> Add(YorumlarPostRequest commentAddDto)
        {
            int sayi = commentAddDto.DersIcerikleriID;
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(commentAddDto);
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Yorumlar", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Ders", new { id = sayi });
            }
            else
            {
                return RedirectToAction("YetkisizIslem");
            }
        }
        public async Task<IActionResult> Son3Ders(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri/son3ders/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<DersIcerikleriDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> YorumlariListele(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Yorumlar/yorumlar/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public class Class1
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [HttpGet]
        public async Task<IActionResult> Konular(int id, int page = 1)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Konular/konular/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<KonularDto>>(jsonString);

            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Dersler/dersler/" + id);
            var jsonString2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.kategori = jsonString2;
            var responseMessage3 = await httpClient.GetAsync("https://localhost:7179/api/Dersler/" + id);
            var jsonString3 = await responseMessage3.Content.ReadAsStringAsync();
            Class1 m = JsonConvert.DeserializeObject<Class1>(jsonString3);
            ViewBag.id = id;
            ViewBag.dersicerigi = m.Name;
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }


        public async Task<IActionResult> DersIcerikleri(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            ViewBag.id = id;
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri/konular/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<IEnumerable<DersIcerikleriDto>>(jsonString);
            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Test/konu/" + id);
            var jsonString2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<IEnumerable<TestDto>>(jsonString2);
            var responseMessage3 = await httpClient.GetAsync("https://localhost:7179/api/Konular/Konuid?id=" + id);
            var jsonString3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.dersad = jsonString3;
            KonuIcerikleris cs = new KonuIcerikleris();
            cs.dersIcerikleriDtos = values;
            cs.Tests = values2;
            if (cs != null)
                return View(cs);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> SoruCevap(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Sorular/dersleregore/" + id);
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

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Sorular/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<SorularDto>(jsonEmployee);
                ViewBag.id = id;
                if (values != null && Token != null)
                    return View(values);
                else
                    return RedirectToAction("GirisYap", "Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CevapYaz(SorularinCevaplariPostRequest p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/SorularinCevaplari", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return View();
            }
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> CevapEkle(SorularinCevaplariPostRequest commentAddDto)
        {
            int sayi = commentAddDto.SorularID;
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(commentAddDto);
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/SorularinCevaplari", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SoruDetay", new { id = sayi });
            }
            return View(commentAddDto);
        }
        public IActionResult HataSayfasi()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Test(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");


            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Test/CozumKontrol/" + id);
            if (responseMessage2.IsSuccessStatusCode)
            {
                var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/TestSoru/testsorulari/" + id);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<TestSoruDto>>(jsonString);
                ViewBag.id = id;
                if (values != null)
                    return View(values);
                else
                    return RedirectToAction("GirisYap", "Login");
            }
            else
                return RedirectToAction("TestCevaplarim", "Ogrenci", new { id = id });



        }
        [HttpPost]
        public async Task<IActionResult> Test(int id, TestCevapPostRequest p)
        {
            var a = Request.Form.ToList();
            int b = Request.Form.Count;
            var deger = Request.Form.ToList();
            foreach (var x in deger)
            {
                p.TestSoruId = int.Parse(x.Key);
                p.CevapId = int.Parse(x.Value);
                p.TestId = id;
                var httpClient = new HttpClient();
                var jsonEmployee = JsonConvert.SerializeObject(p);
                var Token = Request.Cookies["tokenim"];
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
                StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
                var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/TestCevap", content);
            }
            return RedirectToAction("TestCevaplarim", "Ogrenci", new { id = p.TestId });
        }
        public IActionResult Hata()
        {
            return View();
        }
        public IActionResult YetkisizIslem()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Deneme()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Deneme(TestCevapPostRequest p)
        {
            var a = Request.Form.ToList();
            int b = Request.Form.Count;

            foreach (var x in a)
            {
                b--;
                if (b > 0)
                {
                    p.TestSoruId = int.Parse(x.Key);
                    p.CevapId = int.Parse(x.Value);
                }
            }
            return View();
        }
    }
}