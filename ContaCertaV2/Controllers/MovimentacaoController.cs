using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.DTO.Movimentacao;
using ServicesContracts.Enums;

namespace ContaCertaV2.Controllers;

public class MovimentacaoController : Controller
{
    private IMovimentacaoService _movimentacaoService;

    public MovimentacaoController(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }
    
    [Route("home/movimentacoes")]
    public IActionResult Index()
    {
        return View();
    }
    
    [Route("home/movimentacoes/{tipoMovto}")]
    public async Task<IActionResult> Movimentos(string? tipoMovto)
    {
        string? usuarioId = HttpContext.Session.GetString("Id");
        
        if (string.IsNullOrEmpty(tipoMovto))
            return RedirectToAction("Index", "Home");
        
        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção Encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }

        List<MovimentacaoResponse> resultado = new List<MovimentacaoResponse>();

        switch (tipoMovto)
        {
            case "ent":
                resultado = await _movimentacaoService.ListarMovimentacoesPorTipo(new Guid(usuarioId), "Entrada");
                ViewBag.TipoMovto = "Movimentos de Entrada";
                break;
            
            case "sai":
                resultado = await _movimentacaoService.ListarMovimentacoesPorTipo(new Guid(usuarioId), "Saida");
                ViewBag.TipoMovto = "Movimentos de Saída";
                break;
            
            case "geral":
                resultado = await _movimentacaoService.ListarMovimentacoes(new Guid(usuarioId));
                ViewBag.TipoMovto = "Movimentação Geral";
                break;
        }
        
        return View(resultado);
    }

    [HttpPost]
    [Route("/home/movimentacoes/novoMovto")]
    public async Task<IActionResult> NovoMovto(string nome, string descricao, DateTime dataMovto, float valor, string tipoMovto)
    {
        string? usuarioId = HttpContext.Session.GetString("Id");

        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção Encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }

        MovimentacaoAddRequest novoMovto = new MovimentacaoAddRequest()
        {
            UsuarioId = new  Guid(usuarioId),
            Nome = nome,
            Descricao = descricao,
            DataMovto = Convert.ToDateTime(dataMovto),
            Valor = valor,
            TipoMovto = Enum.Parse<TipoMovimentacao>(tipoMovto)
        };
        
        if (novoMovto.ToString() == null)
            return RedirectToAction("Index");

        await _movimentacaoService.NovaMovimentacao(novoMovto);
        
        return RedirectToAction("Index");
    }
    
    [Route("/home/movimentacoes/removerMovto/{movtoId}")]
    public async Task<IActionResult> RemoverMovto(string movtoId)
    {
        string? usuarioId = HttpContext.Session.GetString("Id");

        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção Encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }

        if (string.IsNullOrEmpty(movtoId))
            return RedirectToAction("Index");
        
        await _movimentacaoService.ExcluirMovimentacao(new  Guid(movtoId));
        return RedirectToAction("Index");
    }
}