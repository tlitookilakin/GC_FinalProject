using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController(FinalProjectDbContext context) : ControllerBase
	{
		[Authorize]
		[HttpGet()]
		public IActionResult GetAll()
		{
			string userID = User.Identity.GetId();
			return Ok(context.Recipes.Where(recipe => recipe.UserId == userID).Include(recipe => recipe.RecipeIngredients));
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			if (context.Recipes.Find(id) is Recipe recipe)
			{
				context.Entry(recipe).Collection(recipe => recipe.RecipeIngredients).Load();
				return Ok(recipe);
			}

			return NotFound();
		}

		[Authorize]
		[HttpPost]
		public IActionResult Post([FromBody] Recipe recipe)
		{
			recipe.Id = 0;
			foreach (RecipeIngredient ingredient in recipe.RecipeIngredients)
			{
				ingredient.RecipeId = 0;
				ingredient.Id = 0;
			}
			recipe.UserId = User.Identity.GetId();
			context.Recipes.Add(recipe);
			context.SaveChanges();
			return Created($"api/recipes/{recipe.Id}", recipe);
		}

		[Authorize]
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Recipe recipe)
		{
			if (id != recipe.Id)
				return BadRequest("Id does not match");

			if (recipe.UserId != User.Identity.GetId())
				return BadRequest("Recipe not owned by user");

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

		[Authorize]
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (context.Recipes.Find(id) is Recipe recipe)
			{
				if (recipe.UserId != User.Identity.GetId())
					return BadRequest("Recipe not owned by user");

				context.Recipes.Remove(recipe);
				context.SaveChanges();
				return NoContent();
			}

			return NotFound();
		}
	}
}
