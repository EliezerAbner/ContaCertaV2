using Microsoft.AspNetCore.Mvc;

namespace ContaCertaV2.Controllers;

public class MovimentacaoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}