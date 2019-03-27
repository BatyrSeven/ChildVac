using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChildVac.WebApi.Infrastructure;
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
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

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

            // create JWT-token
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

            // serialize response
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [NonAction]
        private ClaimsIdentity GetIdentity(Token token)
        {
            User user = null;

            switch (token.Role)
            {
                case "Admin":
                    user = _context.Admins.FirstOrDefault(x =>
                        x.Login == token.Login && x.Password == token.Password && x.Role == token.Role);
                    break;
                case "Child":
                    user = _context.Children.FirstOrDefault(x =>
                        x.Login == token.Login && x.Password == token.Password && x.Role == token.Role);
                    break;
                case "Doctor":
                    user = _context.Doctors.FirstOrDefault(x =>
                        x.Login == token.Login && x.Password == token.Password && x.Role == token.Role);
                    break;
                case "Parent":
                    user = _context.Parents.FirstOrDefault(x =>
                        x.Login == token.Login && x.Password == token.Password && x.Role == token.Role);
                    break;
            }

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                var claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            // user not found
            return null;
        }
    }
}
