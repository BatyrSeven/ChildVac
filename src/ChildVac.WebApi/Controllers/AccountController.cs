using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var identity = GetIdentity(token, out var errorMessage);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(errorMessage);
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
                login = identity.Name,
                role = identity.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .FirstOrDefault()
            };

            // serialize response
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [NonAction]
        private ClaimsIdentity GetIdentity(Token token, out string errorMessage)
        {
            errorMessage = null;
            User user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Iin == token.Login);

            if (user == null)
            {
                errorMessage = "User not found";
                return null;
            }

            if (user.Password != token.Password)
            {
                errorMessage = "Invalid login or password";
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Iin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
