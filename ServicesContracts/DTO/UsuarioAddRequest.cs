using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServicesContracts.DTO;

public class UsuarioAddRequest
{
    [Required(ErrorMessage = "É necessário um nome")]
    public string? Nome { get; set; }
    
    [Required(ErrorMessage = "É necessário um E-mail")]
    [EmailAddress(ErrorMessage = "E-mail invalido")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "É necessária a senha")]
    public string? Senha { get; set; }
    
    public string? Salt { get; set; }

    /// <summary>
    /// Realiza a conversão de UsuarioAddRequest para Usuario
    /// </summary>
    /// <returns></returns>
    public Usuario ToUsuario()
    {
        return new Usuario()
        {
            Email = Email,
            Nome = Nome,
            Senha = Senha,
            Salt = Salt
        };
    }
}