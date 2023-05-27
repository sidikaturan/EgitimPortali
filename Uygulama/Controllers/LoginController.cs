using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Authenticate;
using EgitimPortali.Request.Kullanicilar;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace Uygulama.Controllers
{
    public class LoginController : Controller
    {
        public class AuthenticateRequest
        {
            [Required]
            public string Mail { get; set; }
            [Required]
            public string Sifre { get; set; }
        }
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AuthenticateRequest p, string returnUrl = "")
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Auth/login", content);
           
            if (responseMessage.IsSuccessStatusCode)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true
                };
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GirisIcın>(jsonString);

                Response.Cookies.Append("tokenim", values.jwt, cookieOptions);
                List<Claim> userClaims = new List<Claim>();
                foreach (var item in values.roles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, item));
                }
                var identity = new ClaimsIdentity(userClaims,
           CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(identity));
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {           
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("AnaSayfa", "MerkezeTeget");
                }
            }
            return View(p);
        }


        [HttpGet]
        public IActionResult KayitOl()
        {
            ViewBag.v = Request.Cookies["tokenim"];

            return View();
        }
     
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("Uygulama.auth");
            return RedirectToAction("GirisYap");
        }
        public async Task<IActionResult> KayitOl(KullanicilarPostRequest p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7179/api/Auth/register", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GirisYap");
            }
            return View(p);
        }
    }
}
