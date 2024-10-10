using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController(FinalProjectDbContext context) : ControllerBase
	{
		[HttpGet()]
		public IActionResult GetAll(int userID = 0)
		{
			if (userID != 0)
				return Ok(context.Recipes.Where(recipe => recipe.UserId == userID));

			return Ok(context.Recipes);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			if (context.Recipes.Find(id) is Recipe recipe)
				return Ok(recipe);

			return NotFound();
		}

		[HttpPost]
		public IActionResult Post([FromBody] Recipe recipe)
		{
			recipe.Id = 0;
			foreach (RecipeIngredient ingredient in recipe.RecipeIngredients)
			{
				ingredient.RecipeId = 0;
				ingredient.Id = 0;
			}
			context.Recipes.Add(recipe);
			context.SaveChanges();
			return Created($"api/recipes/{recipe.Id}", recipe);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Recipe recipe)
		{
			if (id != recipe.Id)
				return BadRequest("Id does not match");

			if (context.Recipes.FirstOrDefault(recipe => recipe.Id == id) is Recipe old)
			{
				List<int> knownIds = [];

				foreach (RecipeIngredient ingredient in recipe.RecipeIngredients)
				{
					if (!old.RecipeIngredients.Any(i => i.Id == ingredient.Id))
						ingredient.Id = 0;
					ingredient.RecipeId = id;
					knownIds.Add(ingredient.Id);
				}

				List<RecipeIngredient> toRemove = [];

				foreach (RecipeIngredient ingredient in old.RecipeIngredients)
					if (!knownIds.Contains(ingredient.Id))
						toRemove.Add(ingredient);

				context.RecipeIngredients.RemoveRange(toRemove);

				context.Recipes.Update(recipe);
				context.SaveChanges();
				return Ok(recipe);
			}

			return NotFound();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (context.Recipes.Find(id) is Recipe recipe)
			{
				context.Recipes.Remove(recipe);
				context.SaveChanges();
				return NoContent();
			}

			return NotFound();
		}
	}
}
