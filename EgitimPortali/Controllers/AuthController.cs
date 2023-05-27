using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Repository.Kullanici;
using EgitimPortali.Repository.KullaniciRol;
using EgitimPortali.Request.Authenticate;
using EgitimPortali.Request.Kullanicilar;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EgitimPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IKullaniciRepository _userRepository;
        private readonly IKullaniciRolRepository _userRoleRepository;

        public AuthController(IConfiguration configuration, IKullaniciRepository userRepository, IKullaniciRolRepository userRoleRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

      

        [HttpPost("login")]
        public async Task<ActionResult<GirisIcın>> Login(AuthenticateRequest request)
        {

            if (request is null)
            {
                return BadRequest("Lütfen geçerli bir kullanıcı adı giriniz.");
            }

            int login = _userRepository.Login(request);
            if (login != 0)
            { 
                GirisIcın cs = new GirisIcın();

                var deger = _userRepository.KullaniciGetir(login);
                var roles = _userRoleRepository.GetRoleByUserId(deger.Id);
                string token = CreateToken(deger);
                var refreshToken = GenerateRefreshToken(login);
                cs.jwt = token;
                cs.roles = roles.ToList();

                SetRefreshToken(refreshToken);
            
                return Ok(cs);
            }
            else
            {
                return BadRequest("Giriş başarısız oldu");
            }
        }
                 
        [HttpPost("register")]
        public bool Register([FromBody] KullanicilarPostRequest userPostRequest)
        {
            var deger = _userRepository.Register(userPostRequest);
            if (deger != null)
            {
                _userRoleRepository.YeniRolEkle(deger.Id);
                return true;
            }
            return false;
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(int id)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _userRepository.GetByIdRefreshId(id);
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var newtokenuser = _userRepository.KullaniciGetir(id);
            string token = CreateToken(newtokenuser);
            var newRefreshToken = GenerateRefreshToken(id);
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }
        private RefreshToken GenerateRefreshToken(int id)
        {
            var refreshToken = new RefreshToken
            {
                Id = id,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            _userRepository.TokenChange(newRefreshToken);

        }
        private string CreateToken(KullaniciReadDto user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

            var roles = _userRoleRepository.GetRoleByUserId(user.Id);
            
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(identity));
            return jwt;
        }
    }
}