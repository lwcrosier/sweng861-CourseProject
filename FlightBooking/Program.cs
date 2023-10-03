using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;
using FlightBooking.Configurations;
using FlightBooking.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddScoped<ITripSearchResultsRepository, TripSearchResultsRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

// Swagger for testing api
//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>( opt =>
    opt.UseSqlite("Filename=E:\\_SCHOOL\\MVC\\FlightBooking\\Database\\flightbooking.db"));
//builder.Services.AddDbContext<AppDbContext>(opt =>
//    opt.UseSqlite("Filename=Database/flightbooking.db"));


// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuring the TripAdvisorApi Service

/*
var rapidApiConfig = builder.Configuration
        .GetSection("RapidApiConfiguration")
        .Get<RapidApiConfiguration>();
        
builder.Services.AddSingleton(rapidApiConfig);
*/

var apiConfig = builder.Configuration
        .GetSection("RapidApiConfiguration")
        .Get<TripAdvisorSearchConfiguration>();



builder.Services.AddSingleton<TripAdvisorSearchConfiguration>(apiConfig);
builder.Services.AddTransient<ITripAdvisorSearchService, TripAdvisorSearchService>();


builder.Services.AddControllers();


//builder.Services.Configure<TripAdvisorSearchConfiguration>(builder.Configuration.GetSection("RapidApiConfiguration"));
//builder.Services.AddSingleton<TripAdvisorSearchConfiguration>();
//builder.Services.AddTransient<ITripAdvisorSearchService, TripAdvisorSearchService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Add Swagger for testing api
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*
app.MapControllerRoute(
    name: "api",
    pattern: "{controller=TripApi}/{action=Index}/{id?}"
);
*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SavedTrips}/{action=Index}/{id?}"
);


app.Run();

/*
public void ConfigureServices(IServiceCollection services)
{
    var emailConfig = Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
    services.AddSingleton(emailConfig);
    services.AddControllers();
}
*/
