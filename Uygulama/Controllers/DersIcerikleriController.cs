using EgitimPortali.DTO;
using EgitimPortali.Request.DersIcerikleri;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text;

namespace Uygulama.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DersIcerikleriController : Controller
    {
        public async Task<IActionResult> Derslerim()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<DersIcerikleriDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> DersGuncelle(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Konular");
            var jsonString = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<KategoriDto>>(jsonString);
            List<SelectListItem> categoryvalue = (from x in values2
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.Id.ToString()

                                                  }).ToList();
            ViewBag.cv = categoryvalue;
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<DersIcerikleriDto>(jsonEmployee);
                return View(values);
            }
            return RedirectToAction("Derslerim");
        }
        [HttpPost]
        public async Task<IActionResult> DersGuncelle(DersIcerikleriDto p)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var jsonEmployee = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7179/api/DersIcerikleri/" + p.Id, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Derslerim");
            }
            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> YeniDersEkle()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Konular");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Kategoriler>>(jsonString);
            if (values != null)
            {
                List<SelectListItem> categoryvalue = (from x in values
                                                      select new SelectListItem
                                                      {
                                                          Text = x.Name,
                                                          Value = x.Id.ToString()

                                                      }).ToList();
                ViewBag.cv = categoryvalue;
                return View();
            }
            else
                return RedirectToAction("GirisYap", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> YeniDersEkle(DersIcerikleriPostRequest p)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Konular");
            var jsonString = await responseMessage2.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Kategoriler>>(jsonString);
            List<SelectListItem> categoryvalue = (from x in values
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.Id.ToString()

                                                  }).ToList();
            ViewBag.cv = categoryvalue;

            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/DersIcerikleri", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Derslerim");
            }
            return View(p);
        }
        public class Kategoriler
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public async Task<IActionResult> DersSil(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.DeleteAsync("https://localhost:7179/api/DersIcerikleri/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Derslerim");
            }
            return View();
        }
    }
}