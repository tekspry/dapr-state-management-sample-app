using ecom.product.application.ProductApp;
using ecom.product.database.ProductDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IProductApplication, ProductApplication>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
