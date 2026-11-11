namespace Entities;

public class Movimentacao
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public float? Valor { get; set; }
    public string? Descricao { get; set; }
    public string? Nome { get; set; }
    public DateTime DataMovto { get; set; }
    public string? TipoMovto { get; set; }
}