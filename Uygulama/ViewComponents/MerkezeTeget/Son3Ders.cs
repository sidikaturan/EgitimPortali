using EgitimPortali.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Uygulama.ViewComponents.MerkezeTeget
{
    public class Son3Ders : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var httpClient = new HttpClient();
            var Token = Request.Cookies["tokenim"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{Token}");
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/DersIcerikleri/son3ders/" + id);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<DersIcerikleriDto>>(jsonString);
            return View(values);
        }
    }
}
