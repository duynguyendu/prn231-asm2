using Asm2.eBookStore.Client.Middlewares;
using Microsoft.OData.Client;
using Asm2.eBookStore.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<AuthenticationMiddleware>();
builder.Services.AddTransient<AuthorizationMiddleware>();
builder.Services.AddSingleton(new DataServiceContext(new Uri("http://localhost:5098/odata")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

// app.UseMiddleware<AuthenticationMiddleware>();
// app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
