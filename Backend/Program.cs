
using FinalProjectBackend.Models;
using FinalProjectBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Configuration.AddJsonFile("secrets.json", true);

			// CORS
			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					policy =>
					{
						//replace localhost with yours
						//also add your deployed website
						policy.WithOrigins("http://localhost:4200",
										   "https://witty-field-0d4fcb20f.5.azurestaticapps.net")
							.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
					});
			});


			// Add services to the container.
			builder.Services.AddHttpClient<SpoonacularService>();
			builder.Services.AddDbContext<LoginContext>(
				options => options.UseSqlServer("name=loginConnection")
			);
			builder.Services.AddDbContext<FinalProjectDbContext>(
				options => options.UseSqlServer("name=connection"));

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddCookiePolicy(policy => policy.Secure = CookieSecurePolicy.SameAsRequest).AddAuthorization();
			builder.Services.AddIdentityApiEndpoints<IdentityUser>()
				.AddEntityFrameworkStores<LoginContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCookiePolicy();

			app.MapControllers();
			app.MapGroup("/api/user").RequireCors().MapIdentityApi<IdentityUser>();

			// CORS
			app.UseCors();
			app.UseAuthorization();

			app.Run();
		}
	}
}
