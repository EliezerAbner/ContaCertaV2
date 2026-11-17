using Microsoft.AspNetCore.Mvc;

namespace ContaCertaV2.Controllers;

public class HomeController : Controller
{
    [Route("/home")]
    public IActionResult Index()
    {
        string? usuarioId = HttpContext.Session.GetString("Id");
        
        if(string.IsNullOrEmpty(usuarioId))
            return RedirectToAction("Index", "Login");
        
        return View();
    }
    
    [Route("/home/enviarEmail")]
    public IActionResult EnviarEmail()
    {
        //Todo
        return View();
    }
    
    [Route("/home/download")]
    public IActionResult Download()
    {
        return RedirectToAction("Index");
    }
    
    
    [Route("/home/logout")]
    public IActionResult Sair()
    {
        //a fazer: remover sessão
        
        return RedirectToAction("Index", "Login");
    }
}