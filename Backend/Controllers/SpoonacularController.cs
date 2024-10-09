using FinalProjectBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBackend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class SpoonacularController : Controller
{
   private readonly SpoonacularService spoonacularService;

   public SpoonacularController(SpoonacularService spoonacularService)
   {
      this.spoonacularService = spoonacularService;
   }

   [HttpGet("{food}")]
   public async Task<IActionResult> GetFoodResults(string food)
   {
      return Ok(await spoonacularService.GetResults(food)); 
   }
}