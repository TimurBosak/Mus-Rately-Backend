using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Repositories;
using Mus_Rately.WebApp.Repositories.Interfaces;
using Mus_Rately.WebApp.Services.Authentication;
using Mus_Rately.WebApp.Services.Implementation;
using Mus_Rately.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("|DataDirectory|", AppDomain.CurrentDomain.BaseDirectory);
builder.Services.AddDbContext<MusRatelyContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, Role>(
        o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = false;
        })
    .AddSignInManager<SignInManager<User>>()
    .AddUserStore<UserStore>()
    .AddRoleStore<RoleStore>()
    .AddClaimsPrincipalFactory<MusRatelyUserClaimsPrincipalFactory>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
