using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesContracts;
using ServicesContracts.DTO;
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

    [Route("/login")]
    public async Task<IActionResult> FazerLogin(string email, string senha)
    {
        return View();
    }

    [HttpPost]
    [Route("/novaConta")]
    public async Task<IActionResult> NovaConta(UsuarioAddRequest? novoUsuario)
    {
        return View();
    }
}