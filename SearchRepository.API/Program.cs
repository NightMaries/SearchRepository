using SearchRepository.API.Interfaces;
using SearchRepository.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SearchRepositoryContext>();
builder.Services.AddScoped<IConvertToJsonService, ConvertToJsonService>();
builder.Services.AddScoped<IConnectionGitHubService, ConnectionGitHubService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
