using ClosedXML.Excel;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServicesContracts;
using ServicesContracts.DTO.Movimentacao;
using ServicesContracts.Results;

namespace ContaCertaV2.Controllers;

public class DownloadController : Controller
{
    private readonly IMovimentacaoService _movimentacaoService;
    private readonly IExcelService _excelService;
    private readonly IEmailService _emailService;

    public DownloadController(IMovimentacaoService movimentacaoService,  IExcelService excelService, IEmailService emailService)
    {
        _movimentacaoService = movimentacaoService;
        _excelService = excelService;
        _emailService = emailService;
    }
    
    [Route("/home/download")]
    public IActionResult Index()
    {
        string? usuarioId = HttpContext.Session.GetString("Id");

        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }
        
        return View();
    }

    [Route("/home/download/PDF")]
    public async Task<IActionResult> DownloadPdf()
    {
        string? usuarioId = HttpContext.Session.GetString("Id");

        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }
        
        List<MovimentacaoResponse> movimentacao = 
            await _movimentacaoService.ListarMovimentacoes(new Guid(usuarioId));

        return new ViewAsPdf("DownloadPdf", movimentacao, ViewData)
        {
            FileName = "movimentacao.pdf",
            
        };
    }
    
    [Route("/home/download/XLSX")]
    public async Task<IActionResult> DownloadXlsx()
    {
        string? usuarioId = HttpContext.Session.GetString("Id");

        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }

        Result<MemoryStream>? resultado = await _excelService.GerarArquivoExcel(new Guid(usuarioId));
        
        if (!resultado.Sucesso)
        {
            TempData["Erro"] = resultado.Erro;
            return RedirectToAction("Index", "Login");
        }
        
        MemoryStream? ms = resultado.Objeto;
        return File(ms!, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "movimentacao.xlsx");
    }

    [Route("/home/download/enviarEmail")]
    public async Task<IActionResult> EnviarEmail()
    {
        string? usuarioId = HttpContext.Session.GetString("Id");

        if (string.IsNullOrEmpty(usuarioId))
        {
            TempData["Erro"] = "Seção encerrada. Faça login novamente";
            return RedirectToAction("Index", "Login");
        }

        return null;
    }
}