using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.DTO.Usuario;

namespace ContaCertaV2.Controllers;

public class ConfiguracaoController : Controller
{
    private readonly ILoginService _usuarioService;

    public ConfiguracaoController(ILoginService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    
    [Route("/home/configuracao")]
    public IActionResult Index()
    {
        return View();
    }
    
    [Route("/home/configuracao/conta")]
    public IActionResult ConfiguracaoConta()
    {
        return View();
    }
    
    [Route("/home/configuracao/editarConta")]
    public async Task<IActionResult> ConfiguracaoConta(string nome, string email, string senha)
    {
        UsuarioUpdateRequest editarUsuario = new UsuarioUpdateRequest
        {
            Nome = nome,
            Email = email,
            Senha = senha,
        };
        
        await _usuarioService.AlterarConta(editarUsuario);
        
        return RedirectToAction("Index");
    }
    
    [Route("/home/configuracao/excluirConta")]
    [HttpPost]
    public IActionResult ExcluirConta(string? id)
    {
        if(id == null)
            return RedirectToAction("Index");
        
        _usuarioService.DeletarConta(new Guid(id));
        
        return RedirectToAction("Index", "Login");
    }
    
    //alterar conta
    //excluir conta
    //configuração de e-mail
}