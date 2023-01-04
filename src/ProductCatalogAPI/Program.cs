using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data.Repositories;
using ProductCatalogAPI.Business.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProductCatalogDB");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ProductService>();


var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
