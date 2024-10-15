using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend.Models
{
	public class LoginContext : IdentityDbContext<IdentityUser>
	{
		public LoginContext()
		{
		}

		public LoginContext(DbContextOptions<LoginContext> options) : base(options)
		{
		}
	}
}
