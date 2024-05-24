using _1._API.Mapper;
using _2._Domain;
using _3._Data;
using _3._Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependecy inyection
builder.Services.AddScoped<IClientData, ClientMySqlData>();
builder.Services.AddScoped<IClientDomain, ClientDomain>();
builder.Services.AddScoped<IOrderData, OrderMySqlData>();
builder.Services.AddScoped<IOrderDomain, OrderDomain>();

builder.Services.AddAutoMapper(typeof(RequestToModel)
    ,typeof(ModelToRequest)
    ,typeof(ModelToResponse));

// Connect DB
var connectionString = builder.Configuration.GetConnectionString("OrdersDB");

builder.Services.AddDbContext<OrdersDBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

var app = builder.Build();

//EF
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<OrdersDBContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();