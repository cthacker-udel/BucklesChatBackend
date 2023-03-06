using BucklesChatBackend.Database;
using BucklesChatBackend.Repositories;
using BucklesChatBackend.Secrets;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

string developmentCorsSpecifications = "development";

builder.Services.AddCors(options =>
{
    options.AddPolicy(developmentCorsSpecifications, policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.WithMethods("*");
        policy.WithHeaders("*");

    });
});

// PSql connection

IConfigurationSection psqlOptions = configuration.GetSection("DatabaseOptions").GetSection("PostGres");
NpgsqlConnectionStringBuilder npgConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
npgConnectionStringBuilder.Username = psqlOptions.GetValue<string>("Username");
npgConnectionStringBuilder.Password = psqlOptions.GetValue<string>("Password");
npgConnectionStringBuilder.Host = psqlOptions.GetValue<string>("Host");
npgConnectionStringBuilder.Port = psqlOptions.GetValue<int>("Port");
npgConnectionStringBuilder.Database = psqlOptions.GetValue<string>("Database");
npgConnectionStringBuilder.Pooling = psqlOptions.GetValue<bool>("Pooling");
npgConnectionStringBuilder.MinPoolSize = psqlOptions.GetValue<int>("MinPoolSize");
npgConnectionStringBuilder.MaxPoolSize = psqlOptions.GetValue<int>("MaxPoolSize");
npgConnectionStringBuilder.ConnectionLifetime = psqlOptions.GetValue<int>("ConnectionLifetime");

string? mongoDbConnectionString = configuration.GetSection("DatabaseOptions").GetSection("MongoDB").GetValue<string>("ConnectionString");
SecretKeys secretKeysInstance = new SecretKeys(npgConnectionStringBuilder.ToString(), mongoDbConnectionString ?? "");
builder.Services.AddSingleton<ISecretKeys>(secretKeysInstance);

MongoClient mongoDBClient = new MongoClient(mongoDbConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoDBClient);

// MongoDB Connection

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(npgConnectionStringBuilder.ToString()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseCors(developmentCorsSpecifications);

app.MapControllers();

app.Run();
