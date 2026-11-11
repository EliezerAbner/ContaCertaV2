using Microsoft.AspNetCore.Mvc;

namespace ContaCertaV2.Controllers;

public class HomeController : Controller
{
    [Route("/home")]
    public IActionResult Index()
    {
        return View();
    }
    
    //Tela de movimentações
    //Tela de configurações
    
    [Route("/home/logout")]
    public IActionResult Sair()
    {
        //a fazer: remover sessão
        
        return RedirectToAction("Index", "Login");
    }
}