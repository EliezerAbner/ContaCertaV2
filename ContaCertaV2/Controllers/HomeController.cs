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
    
    [Route("/home/logout")]
    public IActionResult Sair()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Login");
    }
}