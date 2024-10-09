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

   [HttpGet("search/{food}")]
   public async Task<IActionResult> GetFoodResults(string food)
   {
      return Ok(await spoonacularService.GetResults(food)); 
   }

   [HttpGet("ingredient/{id}")]
   public async Task<IActionResult> GetSpecificFood(string id, float amount = 0, string? unit = null)
    {
        return Ok(await spoonacularService.GetIngredient(id, amount == 0 ? float.NaN : amount, unit));
    }

}