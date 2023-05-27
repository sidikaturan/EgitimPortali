using EgitimPortali.DTO;
using EgitimPortali.Request.Reklamlar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Uygulama.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ReklamController : Controller
    {
        public async Task<IActionResult> ButunReklamlar()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Reklamlar");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ReklamlarDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        [HttpGet]
        public IActionResult YeniReklamEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> YeniReklamEkle(ReklamlarPostRequest p,IFormFile n)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            FileInfo dosyaBilgisi = new FileInfo(n.FileName);
            string dosyaAdi = dosyaBilgisi.Name.Substring(0, dosyaBilgisi.Name.Length - dosyaBilgisi.Extension.Length);

            dosyaAdi += "-" + Guid.NewGuid().ToString().Replace("-", "") + dosyaBilgisi.Extension;

            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileNameWithPath = Path.Combine(path, dosyaAdi);
            p.Gorsel = "/images/" + dosyaAdi;
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Reklamlar", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    n.CopyTo(stream);
                }
                return RedirectToAction("ButunReklamlar");
            }
            return View(p);
        }

        public async Task<IActionResult> ReklamiGoruntule(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Reklamlar/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ReklamlarDto>(jsonEmployee);
                if (values != null)
                    return View(values);
                else
                    return RedirectToAction("GirisYap", "Login");
            }
            return RedirectToAction("ButunReklamlar");
        }
    }
}
