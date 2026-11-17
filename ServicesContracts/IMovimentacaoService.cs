using ServicesContracts.DTO.Movimentacao;

namespace ServicesContracts;

public interface IMovimentacaoService
{
    /// <summary>
    /// Insere a movimentação no BD
    /// </summary>
    /// <param name="novaMovimentacao"></param>
    /// <returns></returns>
    Task<MovimentacaoResponse> NovaMovimentacao(MovimentacaoAddRequest? novaMovimentacao);
    
    /// <summary>
    /// Realiza alterações de uma movimentação no BD
    /// </summary>
    /// <param name="alteraMovimentacao"></param>
    /// <returns></returns>
    Task<MovimentacaoResponse> AlterarMovimentacao(MovimentacaoUpdateRequest? alteraMovimentacao);
    
    /// <summary>
    /// Realiza a exclusão de uma movimentação no BD
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExcluirMovimentacao(Guid? id);
    
    /// <summary>
    /// Traz todas as movimentações realizadas por determinado usuário
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <returns></returns>
    Task<List<MovimentacaoResponse>> ListarMovimentacoes(Guid? usuarioId);
    
    /// <summary>
    /// Traz todas as movimentações de um determinado tipo realizadas por determinado usuário
    /// </summary>
    /// <param name="usuarioId"></param>
    /// /// <param name="tipoMovto"></param>
    /// <returns></returns>
    Task<List<MovimentacaoResponse>> ListarMovimentacoesPorTipo(Guid? usuarioId, string? tipoMovto);
    
    /// <summary>
    /// Busca determinada movimentação pelo Id dela
    /// </summary>
    /// <param name="movimentacaoId"></param>
    /// <returns></returns>
    Task<MovimentacaoResponse?> BuscaMovimentacao(Guid? movimentacaoId);
}