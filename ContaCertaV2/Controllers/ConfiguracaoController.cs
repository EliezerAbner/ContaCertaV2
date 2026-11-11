using Microsoft.AspNetCore.Mvc;

namespace ContaCertaV2.Controllers;

public class ConfiguracaoController : Controller
{
    [Route("/config")]
    public IActionResult Configuracao()
    {
        return View();
    }
    
    //alterar conta
    //excluir conta
    //configuração de e-mail
}