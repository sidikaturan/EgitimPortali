using EgitimPortali.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Uygulama.ViewComponents.MerkezeTeget
{
    public class Cevaplar : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:7179/api/SorularinCevaplari/cevaplar/" + id);       
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<SoruCevapDto>>(jsonString);
            return View(values);
        }
    }
}
