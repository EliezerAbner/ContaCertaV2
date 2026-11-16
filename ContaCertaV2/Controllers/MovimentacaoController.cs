using Microsoft.AspNetCore.Mvc;

namespace ContaCertaV2.Controllers;

public class MovimentacaoController : Controller
{
    [Route("home/movimentacoes")]
    public IActionResult Index()
    {
        return View();
    }
}