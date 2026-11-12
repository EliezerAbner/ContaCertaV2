using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Movimentacao
{
    [Key]
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public float? Valor { get; set; }
    
    [StringLength(200)]
    public string? Descricao { get; set; }
    
    [StringLength(50)]
    public string? Nome { get; set; }
    public DateTime DataMovto { get; set; }
    
    [StringLength(20)]
    public string? TipoMovto { get; set; }
}