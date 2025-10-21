using CornerStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//endpoints go here

// cashiers

// - Create
app.MapPost("/api/cashiers", (CornerStoreDbContext db, Cashier cashier) =>
{
    db.Cashiers.Add(cashier);
    db.SaveChanges();
    return Results.Created($"/api/cashiers/{cashier.Id}", cashier);
});
//

// - Get (include orders and orders' products by id)
app.MapGet("/api/cashiers/{id}", (CornerStoreDbContext db, int id) =>
{
    return db.Cashiers
    .Include(c => c.Orders)
    .ThenInclude(o => o.OrderProducts)
    .Where(c => c.Id == id)
    .FirstOrDefault();
});
//
//

//products

// - Get (idk what search query string param means)
app.MapGet("/api/products", (CornerStoreDbContext db, string? search) => { });
//

// - Create
app.MapPost("/api/products", (CornerStoreDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"api/product/{product.Id}", product);
});
//

// - Update
app.MapPut("/api/products/{id}", (CornerStoreDbContext db, Product update, int id) =>
{
    Product product = db.Products.FirstOrDefault(p => p.Id == id);
    product.ProductName = update.ProductName;
    product.Price = update.Price;
    product.brand = update.brand;
    product.CategoryId = update.CategoryId;
    db.SaveChanges();
    return Results.NoContent();
});
//
//

//orders

// - Get by id (include details products cashier)
app.MapGet("api/orders/{id}", (CornerStoreDbContext db, int id) =>
{
    return db.Orders
    .Include(o => o.cashier)
    .Include(o => o.OrderProducts)
    .ThenInclude(op => op.Product)
    .ThenInclude(p => p.Category)
    .Where(o => o.Id == id)
    .FirstOrDefault();
});
//

// - Get (takes a orderDate that only returns todays orders)
app.MapGet("/api/orders", (CornerStoreDbContext db, string? orderDate) => { });

//

// - Delete
app.MapDelete("/api/orders/{id}", (int id, CornerStoreDbContext db) =>
{
    Order order = db.Orders.FirstOrDefault(o => o.Id == id);
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});
//

// - Create 
app.MapPost("/api/orders", (CornerStoreDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"api/product/{order.Id}", order);
});
//
//
app.Run();

//don't move or change this!
public partial class Program { }
