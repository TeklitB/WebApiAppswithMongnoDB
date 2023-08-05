
using BlogMgtApi.Models;
using BlogMgtApi.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlogMgtApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<BlogSettings>(builder.Configuration.GetSection(nameof(BlogSettings)));

            builder.Services.AddSingleton<IBlogSettings>(sp => sp.GetRequiredService<IOptions<BlogSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(builder.Configuration.GetValue<string>("BlogSettings:ConnectionString")));

            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

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

            app.Run();
        }
    }
}