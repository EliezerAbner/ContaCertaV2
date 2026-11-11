using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesContracts;
using ServicesContracts.DTO;

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

    [Route("/login")]
    public async Task<IActionResult> FazerLogin(string email, string senha)
    {
        UsuarioResponse? usuarioLogado = await _loginService.FazerLogin(email, senha);
        
        return usuarioLogado != null? RedirectToAction("Home", usuarioLogado) : RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/novaConta")]
    public async Task<IActionResult> NovaConta(UsuarioAddRequest? novoUsuario)
    {
        if (!ModelState.IsValid)
        {
            //a fazer: implementar o modelValidation
        }
        
        UsuarioResponse usuarioCriado = await _loginService.NovaConta(novoUsuario);
        return RedirectToAction("Home", usuarioCriado);
    }
}