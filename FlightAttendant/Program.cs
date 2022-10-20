using FlightAttendant.Data;
using FlightAttendant.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<FlightsDbContext>();
builder.Services.AddTransient<IAirportsRepository, AirportsRepository>();
builder.Services.AddTransient<IAirlinesRepository, AirlinesRepository>();
builder.Services.AddTransient<IFlightsRepository, FlightsRepository>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

// Configure the HTTP request pipeline.
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
