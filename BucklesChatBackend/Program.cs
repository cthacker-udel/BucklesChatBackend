using BucklesChatBackend.Database;
using BucklesChatBackend.Repositories;
using BucklesChatBackend.Secrets;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

var developmentCorsSpecifications = "development";

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

var psqlOptions = configuration.GetSection("DatabaseOptions").GetSection("PostGres");
var npgConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
npgConnectionStringBuilder.Username = psqlOptions.GetValue<string>("Username");
npgConnectionStringBuilder.Password = psqlOptions.GetValue<string>("Password");
npgConnectionStringBuilder.Host = psqlOptions.GetValue<string>("Host");
npgConnectionStringBuilder.Port = psqlOptions.GetValue<int>("Port");
npgConnectionStringBuilder.Database = psqlOptions.GetValue<string>("Database");
npgConnectionStringBuilder.Pooling = psqlOptions.GetValue<bool>("Pooling");
npgConnectionStringBuilder.MinPoolSize = psqlOptions.GetValue<int>("MinPoolSize");
npgConnectionStringBuilder.MaxPoolSize = psqlOptions.GetValue<int>("MaxPoolSize");
npgConnectionStringBuilder.ConnectionLifetime = psqlOptions.GetValue<int>("ConnectionLifetime");

var mongoDbConnectionString = configuration.GetSection("DatabaseOptions").GetSection("MongoDB").GetValue<string>("ConnectionString");
var secretKeysInstance = new SecretKeys(npgConnectionStringBuilder.ToString(), mongoDbConnectionString ?? "");
builder.Services.AddSingleton<ISecretKeys>(secretKeysInstance);

var mongoDBClient = new MongoClient(mongoDbConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoDBClient);

// MongoDB Connection

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(npgConnectionStringBuilder.ToString()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

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
