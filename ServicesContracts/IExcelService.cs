using ServicesContracts.Results;

namespace ServicesContracts;

public interface IExcelService
{
    /// <summary>
    /// Gera um arquivo XLSX de todas a movimentações de um determinado usuário
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <returns>Retorna um MemoryStream do arquivo gerado</returns>
    Task<Result<MemoryStream>> GerarArquivoExcel(Guid usuarioId);
}