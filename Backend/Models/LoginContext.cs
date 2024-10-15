using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend.Models
{
	public class LoginContext : IdentityDbContext<IdentityUser>
	{
	}
}
