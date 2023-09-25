using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Service;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Book>("Books");
    builder.EntitySet<Publisher>("Publishers");
    return builder.GetEdmModel();
}

builder.Services.AddControllers();
builder.Services.AddControllers().AddOData((option => option.Select().Filter()
    .Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel())));
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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