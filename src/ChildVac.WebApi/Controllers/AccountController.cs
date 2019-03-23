using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChildVac.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ChildVac.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // TODO: добавить контекст БД и заменить '_parents'
        private readonly List<Parent> _parents = new List<Parent>
        {
            new Parent { Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Parent { Login="qwerty", Password="55555", Role = "user" }
        };

        // GET: api/Account
        [HttpGet]
        public async Task Get()
        {
            Response.StatusCode = 404;
            await Response.WriteAsync("Please, use POST request to authenticate.");
        }

        [HttpGet]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
        }

        // POST: api/Account
        [HttpPost]
        public async Task Post([FromBody] Token token)
        {
            var identity = GetIdentity(token);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid login or password.");
                return;
            }

            var now = DateTime.UtcNow;

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                login = identity.Name
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [NonAction]
        private ClaimsIdentity GetIdentity(Token token)
        {
            Parent parent = _parents.FirstOrDefault(x => x.Login == token.Login && x.Password == token.Password);
            if (parent != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, parent.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, parent.Role)
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
