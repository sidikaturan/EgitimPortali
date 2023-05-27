using EgitimPortali.DTO;
using EgitimPortali.Request.Roller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Uygulama.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RolController : Controller
    {
        public async Task<IActionResult> ButunRoller()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Rol");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<RolDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> RolEkle(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RolEkle(RollerPostRequest p)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Rol", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ButunRoller");
            }
            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> RolGuncelle(int id)
        {
        

            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Rol/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<RollerUpdateRequest>(jsonEmployee);
                if (values != null)
                    return View(values);
                else
                    return RedirectToAction("GirisYap", "Login");
            }
            return RedirectToAction("ButunRoller");
        }
        [HttpPost]
        public async Task<IActionResult> RolGuncelle(RollerUpdateRequest p)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var jsonEmployee = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7179/api/Rol/" + p.Id, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ButunRoller");
            }
            return View(p);
        }
        public async Task<IActionResult> RolSil(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");

            var responseMessage = await httpClient.DeleteAsync("https://localhost:7179/api/Rol/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ButunRoller");
            }
            return View();
        }
    }
}
