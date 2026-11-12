using ServicesContracts.Enums;

namespace ServicesContracts.DTO.Movimentacao;

public class MovimentacaoResponse
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public float? Valor { get; set; }
    public string? Descricao { get; set; }
    public string? Nome { get; set; }
    public DateTime DataMovto { get; set; }
    public string? TipoMovto { get; set; }

    public MovimentacaoUpdateRequest ToMovimentacaoUpdateRequest()
    {
        return new MovimentacaoUpdateRequest
        {
            Id = Id,
            UsuarioId = UsuarioId,
            Valor = Valor,
            Descricao = Descricao,
            Nome = Nome,
            DataMovto = DataMovto,
            TipoMovto = (TipoMovimentacao)Enum.Parse(typeof(TipoMovimentacao), TipoMovto, true)
        };
    }
}

public static class MovimentacaoExtensions
{
    public static MovimentacaoResponse ToMovimentacaoResponse(this Entities.Movimentacao movimentacao)
    {
        return new MovimentacaoResponse
        {
            Id = movimentacao.Id,
            UsuarioId = movimentacao.UsuarioId,
            Valor = movimentacao.Valor,
            Descricao = movimentacao.Descricao,
            Nome = movimentacao.Nome,
            DataMovto = movimentacao.DataMovto,
            TipoMovto = movimentacao.TipoMovto
        };
    }
}