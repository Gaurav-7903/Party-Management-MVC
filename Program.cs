using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Party_Management.Data;
using Party_Management.Models;
using Party_Management.Service;
using Party_Management.ServiceContract;
using ServiceContract;
using Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // Only For Post Action Method
});
//builder.Services.AddControllersWithViews();

// DI Service
builder.Services.AddScoped<IPartyService, PartyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRateService, ProductRateService>();
builder.Services.AddScoped<IProductAssignmentService, ProductAssignmentService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Add Db Service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// Service to Enable to identity Based User
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>() // // Manipulating the User Data
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>(); // Manipulating the Role Data

builder.Services.AddAuthorization();


// Authcation and Authentication and Authrazation Service

builder.Services.AddAuthorization(option =>
{
    // Apply authorization for all the
    option.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // only authenticate user is allowed
    option.AddPolicy("NotAuthorized", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return !context.User.Identity.IsAuthenticated;
        });
    });
});

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/Login";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// PDF Generator Setup
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
