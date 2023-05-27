using EgitimPortali.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Uygulama.Controllers
{
    [Authorize(Roles = "Admin")]

    public class TestController : Controller
    {
        public async Task<IActionResult> ButunTestler()
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/Test");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<TestDto>>(jsonString);
            if (values != null)
                return View(values);
            else
                return RedirectToAction("GirisYap", "Login");
        }
        public async Task<IActionResult> TestiSil(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.DeleteAsync("https://localhost:7179/api/Test/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ButunTestler");
            }
            return View();
        }
    }
}