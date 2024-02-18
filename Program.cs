using BCV_WSCRAP_API.Services;

var builder = WebApplication.CreateBuilder(args);

var configSection = builder.Configuration.GetSection("AppFeatures");

// Add services to the container.
builder.Services.AddSingleton(x => new BankDictionary(builder.Configuration));
builder.Services.AddSingleton(x => new ConnectionStrings(builder.Configuration.GetSection("ConnectionStrings")));
builder.Services.AddTransient(x => new Scrapper(builder.Configuration, x.GetRequiredService<ConnectionStrings>()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings => settings.ConfigObject.AdditionalItems.Add("syntaxHighlight", false));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
