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
		return Ok(dbContext.CreateUserIfAbsent(User.Identity.GetId()));
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateUser([FromBody] User user)
    {
        if (user.Id != User.Identity.GetId())
            return BadRequest();

        dbContext.Users.Update(user);
        dbContext.SaveChanges();
        return Ok(user);
    }
}