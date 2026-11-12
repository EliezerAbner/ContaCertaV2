using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Usuario
{
    [Key]
    public Guid UsuarioId { get; set; }
    
    [StringLength(50)]
    public string? Nome { get; set; }
    
    [StringLength(50)]
    public string? Email { get; set; }
    
    [StringLength(200)]
    public string? Senha { get; set; }
    
    [StringLength(200)]
    public string? Salt { get; set; }
}