
using AccountMgtApi.Models;
using AccountMgtApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AccountMgtApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<BankSettings>(builder.Configuration.GetSection(nameof(BankSettings)));

            builder.Services.AddSingleton<IBankSettings>(sp => sp.GetRequiredService<IOptions<BankSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(builder.Configuration.GetValue<string>("BankSettings:ConnectionString")));

            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IUseBsonDocumentServices, UseBsonDocumentServices>();
            builder.Services.AddScoped<ITransactionServices, TransactionServices>();
            builder.Services.AddScoped<IAggregationServices, AggregationServices>();

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