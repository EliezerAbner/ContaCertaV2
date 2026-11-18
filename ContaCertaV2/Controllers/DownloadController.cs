using ClosedXML.Excel;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServicesContracts;
using ServicesContracts.DTO.Movimentacao;

namespace ContaCertaV2.Controllers;

public class DownloadController : Controller
{
    private readonly IMovimentacaoService _movimentacaoService;

    public DownloadController(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
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
        
        List<MovimentacaoResponse> movimentacao = 
            await _movimentacaoService.ListarMovimentacoes(new Guid(usuarioId));
            
        DataTable dataTable = new DataTable("Movimentacao");
        dataTable.Columns.AddRange(new DataColumn[]
        {
            new DataColumn("Nome"),
            new DataColumn("Valor"),
            new DataColumn("Descrição"),
            new DataColumn("Data da Movimentação", typeof(DateTime))
        });
        
        MemoryStream ms = new MemoryStream();

        try
        {
            foreach (var mov in movimentacao)
            {
                float? valor = mov.TipoMovto == "Entrada" ? mov.Valor : (mov.Valor * -1);
                dataTable.Rows.Add(mov.Nome, valor, mov.Descricao, mov.DataMovto);
            }

            using XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dataTable);
            wb.SaveAs(ms);
        }
        catch (Exception ex)
        {
            HttpContext.Session.Remove("Id");
            TempData["Erro"] = ex.Message;
            RedirectToAction("Index", "Login");
        }
        
        ms.Position = 0;
        
        return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "movimentacao.xlsx");
    }
}