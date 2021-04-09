using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sossalao.Core.Data;
using sossalao.Core.Models;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace sossalao.Core.Controllers
{
	[Route("api/auth")]
	public class AuthController : Controller
	{
		readonly DataBaseContext _context;

		public AuthController(DataBaseContext context)
		{
			_context = context;
		}
		public ClaimsIdentity Identity { get; private set; }

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Post([FromBody] Login login, [FromServices] SigningConfigurations signingConfigurations, [FromServices] TokenConfigurations tokenConfigurations)
		{
			Security security = new Security();
			login.password = security.cryptopass(login.password);
			Login user = _context.TB_Login.FirstOrDefault(us => us.user == login.user && us.password == login.password);
			if (user != null)
			{
				ClaimsIdentity Identity = new ClaimsIdentity(
					new GenericIdentity(user.IdLogin.ToString(), "Login"),
					new[]
					{
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
						new Claim(JwtRegisteredClaimNames.UniqueName, user.IdLogin.ToString()),
						new Claim("Nome", user.user),
						new Claim("Nivel", user.accessLevel.ToString())
					}
				);

				Identity.AddClaim(new Claim(ClaimTypes.Role, user.accessLevel.ToString()));
				DateTime creationDate = DateTime.Now;
				DateTime expirationDate = creationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

				var handler = new JwtSecurityTokenHandler();

				var securityToken = handler.CreateToken(new SecurityTokenDescriptor
				{
					Issuer = tokenConfigurations.Issuer,
					Audience = tokenConfigurations.Audience,
					SigningCredentials = signingConfigurations.SigningCredentials,
					Subject = Identity,
					NotBefore = creationDate,
					Expires = expirationDate
				});
				var token = handler.WriteToken(securityToken);

				var retorno = new
				{
					authenticated = true,
					created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
					expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
					accessToken = token,
					message = "OK"
				};
				return Ok(retorno);
			}
			else
			{
				var retorno = new { authenticated = false, message = "Falha ao autenticar" };
				return BadRequest(retorno);
			}
		}
	}
}
