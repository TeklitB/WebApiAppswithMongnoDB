
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<StudentSettings>(
                builder.Configuration.GetSection(nameof(StudentSettings)));

            builder.Services.AddSingleton<IStudentSettings>(sp =>
                sp.GetRequiredService<IOptions<StudentSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s =>
                    new MongoClient(builder.Configuration.GetValue<string>("StudentSettings:ConnectionString")));

            builder.Services.AddScoped<IStudentService, StudentService>();

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