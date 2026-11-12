using System.ComponentModel.DataAnnotations;

namespace Entities;

public class ConfigEmail
{
    [Required(ErrorMessage = "{0} deve ser preenchido")]
    public string? EnderecoSmtp { get; set; }
    
    [Required(ErrorMessage = "{0} deve ser preenchido")]
    public int PortaSmtp { get; set; }
    
    [Required(ErrorMessage = "{0} deve ser preenchido")]
    public string? Usuario { get; set; }
    
    [Required(ErrorMessage = "{0} deve ser preenchido")]
    public string? Senha { get; set; }
}