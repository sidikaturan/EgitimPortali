using EgitimPortali.DTO;
using EgitimPortali.Request.KullanicilarinRolleri;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Uygulama.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KullaniciController : Controller
    {

        public async Task<IActionResult> ButunKullanicilar()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Kullanici");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<KullaniciDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }


        public async Task<IActionResult> KullanicininYetkileri(int id)
        {
            ViewBag.id = id;
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/KullanicilarinRolleri/kullanicininRolleri/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<KullaniciRolDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }

        [HttpGet]

        public async Task<IActionResult> KullaniciYetkilendirme(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Rol");
            var jsonString = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<RolDto>>(jsonString);
            List<SelectListItem> categoryvalue = (from x in values2
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.Id.ToString()

                                                  }).ToList();
            ViewBag.cv = categoryvalue;
            ViewBag.cv2 = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KullaniciYetkilendirme(KullanicilarinRolleriPostRequest p)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/KullanicilarinRolleri", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("KullanicininYetkileri", new { id = p.KullaniciID });
            }
            
            else
            {
                var responseMessage2 = await httpClient.GetAsync("https://localhost:7179/api/Rol");
                var jsonString = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<List<RolDto>>(jsonString);
                List<SelectListItem> categoryvalue = (from x in values2
                                                      select new SelectListItem
                                                      {
                                                          Text = x.Name,
                                                          Value = x.Id.ToString()

                                                      }).ToList();
                ViewBag.cv = categoryvalue;
                return View(p);
            }
        }
        public async Task<IActionResult> KullaniciSil(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.DeleteAsync("https://localhost:7179/api/Kullanici/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ButunKullanicilar");
            }
            return View();
        }
        public async Task<IActionResult> KullaniciYetkiSil(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.DeleteAsync("https://localhost:7179/api/KullanicilarinRolleri/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ButunKullanicilar");
            }
            return View();
        }
    }
}
