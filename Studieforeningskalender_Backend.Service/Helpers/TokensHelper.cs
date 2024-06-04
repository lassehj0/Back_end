using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Studieforeningskalender_Backend.Domain.Roles;
using Studieforeningskalender_Backend.Domain.Users;
using System.Security.Claims;

namespace Studieforeningskalender_Backend.Service.Helpers
{
	public static class TokensHelper
	{
		public static async void SignInAsync(User user, IList<Role> roles, HttpContext httpContext, bool rememberMe)
		{
			var claims = new List<Claim>
			{
				new Claim("UserName", user.UserName),
				new Claim("Email", user.EmailAddress)
			};
			if ((roles?.Count ?? 0) > 0)
			{
				foreach (var role in roles)
					claims.Add(new Claim(ClaimTypes.Role, role.Name));
			}

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				IsPersistent = rememberMe,
				ExpiresUtc = rememberMe ? DateTime.UtcNow.AddDays(30) : null,
			};

			await httpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}
	}
}
