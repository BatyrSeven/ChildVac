﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ChildVac.WebApi.Infrastructure;
using ChildVac.WebApi.Application.Models;
using ChildVac.WebApi.Application.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChildVac.WebApi.Controllers
{
    /// <summary>
    ///     Used for authentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="logger">NLong logger</param>
        public AccountController(ApplicationContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        ///     Returns the information of User by token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseBaseModel<UserModel>> Get()
        {
            var iin = User?.Identity?.Name;
            var user = AccountHelper.GetUserByIin(_context, iin);

            if (user != null)
            {
                return Ok(new ResponseBaseModel<UserModel>
                {
                    Result = new UserModel
                    {
                        Id = user.Id,
                        Iin = user.Iin,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role.Name
                    }
                });
            }

            return NotFound(
                new MessageResponseModel(false,
                    new MessageModel("По запросу ничего не найдено.",
                        "Для авторизации используйте метод POST.")));
        }

        /// <summary>
        ///     Generates the JWT for authorization
        /// </summary>
        /// <param name="request">TokenRequestModel with authorizaion data</param>
        /// <returns>TokenResponseModel with generated JWT</returns>
        [HttpPost]
        public ActionResult<ResponseBaseModel<TokenResponseModel>> Post(
            [FromBody] TokenRequestModel request)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Iin == request.Iin);

            if (user == null)
            {
                return BadRequest(
                    new MessageResponseModel(false,
                        new MessageModel("Пользователь не найден.",
                            "Проверьте введенные данные и попробуйте снова.")));
            }

            if (user.Password != request.Password)
            {
                return BadRequest(
                    new MessageResponseModel(false,
                        new MessageModel("Пароль введен неверно.",
                            "Проверьте введенные данные и попробуйте снова.")));
            }

            if (user.Role == null || 
                !user.Role.Name.Equals(request.Role, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest(
                    new MessageResponseModel(false,
                        new MessageModel("Пользователь не имеет доступа к системе.",
                            "Проверьте введенные данные и попробуйте снова.")));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Iin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            var identity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            var token = GetJwt(identity);

            return Ok(new ResponseBaseModel<TokenResponseModel>
            {
                Result = new TokenResponseModel
                {
                    User = new UserModel
                    {
                        Id = user.Id,
                        Iin = user.Iin,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role.Name
                    },
                    Token = token
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
