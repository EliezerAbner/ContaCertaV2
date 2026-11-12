using System.ComponentModel.DataAnnotations;
using ServicesContracts.Enums;

namespace ServicesContracts.DTO.Movimentacao;

public class MovimentacaoUpdateRequest
{
    [Required(ErrorMessage = "É necessário um Id")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "A movimentação deve estar atrelada a um usuário")]
    public Guid UsuarioId { get; set; }
    
    public float? Valor { get; set; }
    public string? Descricao { get; set; }
    public string? Nome { get; set; }
    
    [Required(ErrorMessage = "Data da movimentação é necessária")]
    public DateTime DataMovto { get; set; }
    
    [Required(ErrorMessage = "Tipo de movimentação necessária")]
    public TipoMovimentacao? TipoMovto { get; set; }
    
    public Entities.Movimentacao ToMovimentacao()
    {
        return new Entities.Movimentacao()
        {
            Id = Id,
            UsuarioId = UsuarioId,
            Valor = Valor,
            Descricao = Descricao,
            Nome = Nome,
            DataMovto = DataMovto,
            TipoMovto = TipoMovto.ToString()
        };
    }
}