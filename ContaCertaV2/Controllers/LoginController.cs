using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.DTO.Usuario;
using ServicesContracts.Results;

namespace ContaCertaV2.Controllers;

public class LoginController : Controller
{
    private readonly ILoginService _loginService;
    
    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Index(string erro)
    {
        return View(erro);
    }

    [Route("/login")]
    public async Task<IActionResult> FazerLogin(string email, string senha)
    {
        Result<UsuarioResponse>? resultado = await _loginService.FazerLogin(email, senha);
        
        if (!resultado.Sucesso)
            return RedirectToAction("Index");
            //a fazer: mostrar o erro
            
        UsuarioResponse usuarioResponse = resultado.Objeto!;
        HttpContext.Session.SetString("Id", usuarioResponse.UsuarioId.ToString()!);
        return RedirectToAction("Home", "Index");
    }
    
    [HttpGet]
    [Route("/novaConta")]
    public IActionResult NovaConta()
    {
        return View();
    }

    [HttpPost]
    [Route("/novaConta")]
    public async Task<IActionResult> NovaConta(string nome, string email, string senha, string senhaConfirmacao)
    {
        UsuarioAddRequest usuarioAddRequest = new()
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };
        
        Result<UsuarioResponse> resultado = await _loginService.NovaConta(usuarioAddRequest);
        
        if (!resultado.Sucesso)
        {
            return RedirectToAction("Index");
            //a fazer: deverá ser mostrado o erro no front
        }
        
        UsuarioResponse usuarioResponse = resultado.Objeto!;
        HttpContext.Session.SetString("Id", usuarioResponse.UsuarioId.ToString()!);
        return RedirectToAction("Home", "Index");
    }
}