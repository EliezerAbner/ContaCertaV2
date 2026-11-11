using Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using ServicesContracts;
using MySql.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//adição de serviços
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddDbContext<ContaCertaDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("ContaCertaDb")!);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.Run();