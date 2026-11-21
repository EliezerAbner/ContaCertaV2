using System.Data;
using ClosedXML.Excel;
using ServicesContracts;
using ServicesContracts.DTO.Movimentacao;
using ServicesContracts.Results;

namespace Services;

public class ExcelService : IExcelService
{
    private readonly IMovimentacaoService _movimentacaoService;
    
    public ExcelService(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }
    
    public async Task<Result<MemoryStream>> GerarArquivoExcel(Guid usuarioId)
    {
        if(usuarioId == Guid.Empty)
            throw new ArgumentNullException(nameof(usuarioId));
        
        List<MovimentacaoResponse> movimentacao = await _movimentacaoService.ListarMovimentacoes(usuarioId);
        
        DataTable dataTable = new DataTable("Movimentacao");
        dataTable.Columns.AddRange(new DataColumn[]
        {
            new DataColumn("Nome"),
            new DataColumn("Valor"),
            new DataColumn("Descrição"),
            new DataColumn("Data da Movimentação", typeof(DateTime))
        });
        
        foreach (MovimentacaoResponse mov in movimentacao)
        {
            float? valor = mov.TipoMovto == "Entrada" ? mov.Valor : (mov.Valor * -1);
            dataTable.Rows.Add(mov.Nome, valor, mov.Descricao, mov.DataMovto);
        }

        MemoryStream ms = new MemoryStream();
        
        try
        {
            using XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dataTable);
            wb.SaveAs(ms);
        }
        catch (Exception ex)
        {
            ms.Position = 0;
            return Result<MemoryStream>.Falha(ex.Message);
        }
        
        ms.Position = 0;
        return Result<MemoryStream>.Ok(ms);
    }
}