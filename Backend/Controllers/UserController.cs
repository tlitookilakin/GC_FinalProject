using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBackend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : Controller
{
    private FinalProjectDbContext dbContext;

    public UserController(FinalProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetUser()
    {
		IdentityUser identity = (IdentityUser)User.Identity!;

		if (dbContext.Users.Find(identity.Id) is User user)
			return Ok(user);

		user = new() { Id = identity.Id, Name = identity.NormalizedUserName };
		dbContext.Users.Add(user);
		dbContext.SaveChanges();
		return Ok(user);
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateUser([FromBody] User user)
    {
        if (user.Id != ((IdentityUser)User.Identity!).Id)
            return BadRequest();

        dbContext.Users.Update(user);
        dbContext.SaveChanges();
        return Ok(user);
    }
}