using Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using ServicesContracts;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//adição de serviços
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IMovimentacaoService, MovimentacaoService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISenhaService, SenhaService>();

builder.Services.AddDbContext<ContaCertaDbContext>(options =>
{
    string? json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Configuracao", "configDatabase.json"));
    string? stringConexao = JsonConvert.DeserializeObject<string>(json);

    if (stringConexao == null)
        throw new ArgumentException("Falha ao carregar o json de conexão ao banco");
    
    options.UseMySQL(stringConexao);
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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