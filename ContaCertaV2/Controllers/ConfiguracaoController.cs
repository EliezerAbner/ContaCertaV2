using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.DTO.Usuario;
using ServicesContracts.Results;

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
        string? usuarioId = HttpContext.Session.GetString("Id");
        
        if(string.IsNullOrEmpty(usuarioId))
            return RedirectToAction("Index", "Login");
        
        return View();
    }
    
    [Route("/home/configuracao/conta")]
    public async Task<IActionResult> ConfiguracaoConta()
    {
        string? usuarioId = HttpContext.Session.GetString("Id");
        
        if(string.IsNullOrEmpty(usuarioId))
            return RedirectToAction("Index", "Login");
        
        Result<UsuarioResponse> resultado = await _usuarioService.BuscarUsuario(new Guid(usuarioId));
        
        if (!resultado.Sucesso)
            return RedirectToAction("Index", "Login");
        
        UsuarioResponse? usuarioEncontrado = resultado.Objeto;
        
        return View(usuarioEncontrado);
    }
    
    [Route("/home/configuracao/editarConta")]
    public async Task<IActionResult> ConfiguracaoConta(string nome, string email, string senha)
    {
        string? usuarioId = HttpContext.Session.GetString("Id");
        
        if(string.IsNullOrEmpty(usuarioId))
            return RedirectToAction("Index", "Login");
        
        UsuarioUpdateRequest editarUsuario = new UsuarioUpdateRequest
        {
            UsuarioId = new Guid(usuarioId!),
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
        string? usuarioId = HttpContext.Session.GetString("Id");
        
        if(string.IsNullOrEmpty(usuarioId))
            return RedirectToAction("Index", "Login");
        
        if(id == null)
            return RedirectToAction("Index");
        
        _usuarioService.DeletarConta(new Guid(id));
        
        return RedirectToAction("Index", "Login");
    }
}