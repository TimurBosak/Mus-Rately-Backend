using Microsoft.EntityFrameworkCore;
using Mus_Rately.WebApp.Repositories;
using Mus_Rately.WebApp.Repositories.Interfaces;
using Mus_Rately.WebApp.Services.Implementation;
using Mus_Rately.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("|DataDirectory|", AppDomain.CurrentDomain.BaseDirectory);
builder.Services.AddDbContext<MusRatelyContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddScoped<IMusRatelyUnitOfWork, MusRatelyUnitOfWork>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<MusRatelyContext>();

    // Apply any pending migrations
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
