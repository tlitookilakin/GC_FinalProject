using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace FinalProjectBackend
{
	public static class Extensions
	{
		public static string GetId(this IIdentity? identity)
		{
			return (identity as IdentityUser)?.Id ?? "";
		}
	}
}
