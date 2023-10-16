using BookStoreAPI.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
var builder = WebApplication.CreateBuilder(args);
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Book>("Books");
    builder.EntitySet<Press>("Presses");
    builder.EntitySet<Address>("Addresses");
    return builder.GetEdmModel();
}
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConneciton");
builder.Services.AddDbContext<MyDataContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("Odata", GetEdmModel()).Filter().Select().Expand().OrderBy().SetMaxTop(100));
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
