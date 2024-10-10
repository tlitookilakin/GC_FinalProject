
using FinalProjectBackend.Models;
using FinalProjectBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        //replace localhost with yours
                        //also add your deployed website
                        policy.WithOrigins("http://localhost:4200",
                                           "https://dinosaur-lore-store.com")
                            .AllowAnyMethod().AllowAnyHeader();
                    });
            });


            // Add services to the container.
			builder.Services.AddHttpClient<SpoonacularService>();
			builder.Services.AddDbContext<FinalProjectDbContext>(
				options => options.UseSqlServer("name=connection"));
			builder.Configuration.AddJsonFile("secrets.json", true);

            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

            // CORS
            app.UseCors();

            app.Run();
		}
	}
}
