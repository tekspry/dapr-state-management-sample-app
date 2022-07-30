using ecom.product.application.ProductApp;
using ecom.product.database.ProductDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IProductApplication, ProductApplication>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson();

var daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
//var daprPort = "5016";
if (String.IsNullOrEmpty(daprPort))
{
    // we're not running in DAPR - use regular service invocation and an in-memory basket
    Console.WriteLine("NOT USING DAPR");    
}
else
{
    Console.WriteLine("USING DAPR");
    builder.Services.AddDaprClient();
    // Using the DAPR SDK to create a DaprClient, in stead of fiddling with URI's our selves

    
}


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
