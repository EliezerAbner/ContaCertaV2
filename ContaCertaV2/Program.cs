using Services;
using ServicesContracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

//adição de serviços
builder.Services.AddScoped<ILoginService, LoginService>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.Run();