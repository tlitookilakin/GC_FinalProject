using FinalProjectBackend.Models;
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

    [HttpGet()]
    public IActionResult GetUserLogin(string name)
    {
        User userInfo = dbContext.Users.FirstOrDefault(user => user.Name == name);
        return Ok(userInfo);
    }

    [HttpPost()]
    public IActionResult CreateAccount([FromBody] User user)
    {
        user.Id = "";
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return Created($"/api/User/{user.Id}", user);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        User userInfo = dbContext.Users.Find(id);
        if (userInfo == null)
        {
            return NotFound();
        }

        return Ok(userInfo); 
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(string id, [FromBody] User user)
    {
        if (user.Id != id)
        {
            return BadRequest();
        }

        if (!dbContext.Users.Any(user => user.Id == id))
        {
            return NotFound();
        }

        dbContext.Users.Update(user);
        dbContext.SaveChanges();
        return Ok(user);
    }
}