using FinalProjectBackend.Models;
using FinalProjectBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController(FinalProjectDbContext context, SpoonacularService spoon) : ControllerBase
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
		public async Task<IActionResult> Post([FromBody] Recipe recipe)
		{
			recipe.Id = 0;
			foreach (RecipeIngredient ingredient in recipe.RecipeIngredients)
			{
				ingredient.RecipeId = 0;
				ingredient.Id = 0;
			}
			recipe.UserId = User.Identity.GetId();
			context.CreateUserIfAbsent(recipe.UserId);

			await CalculateIngredients(recipe);

			context.Recipes.Add(recipe);
			context.SaveChanges();
			return Created($"api/recipes/{recipe.Id}", recipe);
		}

		[Authorize]
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] Recipe recipe)
		{
			if (id != recipe.Id)
				return BadRequest("Id does not match");

			if (recipe.UserId != User.Identity.GetId())
				return BadRequest("Recipe not owned by user");

			if (context.Recipes.FirstOrDefault(recipe => recipe.Id == id) is Recipe old)
			{
				context.CreateUserIfAbsent(recipe.UserId);
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

				await CalculateIngredients(recipe);

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
			if (context.Recipes.Include(nameof(Recipe.RecipeIngredients)).FirstOrDefault(r => r.Id == id) is Recipe recipe)
			{
				if (recipe.UserId != User.Identity.GetId())
					return BadRequest("Recipe not owned by user");

				context.Recipes.Remove(recipe);
				context.SaveChanges();
				return NoContent();
			}

			return NotFound();
		}

		private async Task CalculateIngredients(Recipe recipe)
		{
			var details = await spoon.GetAllIngredients(recipe.RecipeIngredients);

			recipe.Calories = (int)details.Sum(i => i is null ? 0 : i.nutrition.nutrients.First(
				n => n.name.Equals("calories", StringComparison.OrdinalIgnoreCase)
			).amount);

			recipe.Carbs = (int)details.Sum(i => i is null ? 0 : i.nutrition.nutrients.First(
				n => n.name.Equals("carbohydrates", StringComparison.OrdinalIgnoreCase)
			).amount);
		}
	}
}
