
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SchoolMgtSystemApi.Models;
using SchoolMgtSystemApi.Services;

namespace SchoolMgtSystemApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<SchoolDatabaseSettings>(
                builder.Configuration.GetSection(nameof(SchoolDatabaseSettings)));

            builder.Services.AddSingleton<ISchoolDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<SchoolDatabaseSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s =>
                    new MongoClient(builder.Configuration.GetValue<string>("SchoolDatabaseSettings:ConnectionString")));

            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICourseService, CourseService>();

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