using BCV_WSCRAP_API.Services;

var builder = WebApplication.CreateBuilder(args);

var configSection = builder.Configuration.GetSection("AppFeatures");

// Add services to the container.

builder.Services.AddTransient<IScrapper>(x => new Scrapper(builder.Configuration["BrowserRoute"]));
builder.Services.AddSingleton<IBankDictionary>(x => new BankDictionary(builder.Configuration));
builder.Services.AddSingleton<IKeyPhrasesConverter>(x => new KeyPhrasesConverter(builder.Configuration));
builder.Services.AddSingleton<IDataTableConverter>(x => new DataTableConverter(x.GetRequiredService<IKeyPhrasesConverter>()));

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
