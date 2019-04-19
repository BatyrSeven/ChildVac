using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Application.Models;
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
        public ActionResult Get()
        {
            return NotFound(new ErrorResponseModel
            {
                MessageTitle = "По запросу ничего не найдено.",
                MessageText = "Для авторизации используйте метод POST."
            });
        }

        // POST: api/Account
        [HttpPost]
        public ActionResult<ResponseBaseModel<TokenResponseModel>> Post([FromBody] TokenRequestModel request)
        {
            User user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Iin == request.Iin);

            if (user == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageTitle = "Пользователь не найден.",
                    MessageText = "Проверьте введенные данные и попробуйте снова."
                });
            }

            if (user.Password != request.Password)
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageTitle = "Пароль введен неверно.",
                    MessageText = "Проверьте введенные данные и попробуйте снова."
                });
            }

            if (!user.Role.Name.Equals(request.Role, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageTitle = "Пользователь не имеет доступа к системе.",
                    MessageText = "Проверьте введенные данные и попробуйте снова."
                });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Iin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            var identity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            if (identity == null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    MessageTitle = "Извините, произошла ошибка при авторизации.",
                    MessageText = "Попробуйте снова."
                });
            }

            var iin = identity.Name;
            var token = GetJwt(identity);
            var role = identity.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .FirstOrDefault();

            return Ok(new ResponseBaseModel<TokenResponseModel> {
                Result = new TokenResponseModel()
                {
                    Iin = iin,
                    Token = token,
                    Role = role
                }
            });
        }

        [NonAction]
        private string GetJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            // create JWT-token
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                signingCredentials:
                    new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
