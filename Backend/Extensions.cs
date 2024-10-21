using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace FinalProjectBackend
{
	public static class Extensions
	{
		public static string GetId(this IIdentity? identity)
		{
			return (identity as ClaimsIdentity)?.Claims?.FirstOrDefault(
				c => c.Type.EndsWith("nameIdentifier", StringComparison.OrdinalIgnoreCase)
			)?.Value ?? "";
		}

		public static User CreateUserIfAbsent(this FinalProjectDbContext context, string id)
		{
			if (context.Users.Find(id) is User user)
				return user;

			user = new() { Id = id };
			context.Users.Add(user);
			context.SaveChanges();
			return user;
		}
	}
}
