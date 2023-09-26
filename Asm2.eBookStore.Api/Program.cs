using Asm2.eBookStore.Api;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;
using Asm2.eBookStore.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

builder.Services.AddControllers();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<BookDto>("Books");
    builder.EntitySet<PublisherDto>("Publishers");
    builder.EntitySet<AuthorDto>("Authors");
    return builder.GetEdmModel();
}

builder.Services
    .AddControllers()
    .AddOData(
        (
            option =>
                option
                    .Select()
                    .Filter()
                    .Count()
                    .OrderBy()
                    .Expand()
                    .SetMaxTop(100)
                    .AddRouteComponents("odata", GetEdmModel())
        )
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BooksService, BooksService>();
builder.Services.AddScoped<PublishersService, PublishersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
