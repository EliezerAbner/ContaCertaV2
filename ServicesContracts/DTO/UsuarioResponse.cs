using Entities;

namespace ServicesContracts.DTO;

public class UsuarioResponse
{
    public Guid? UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }

    public UsuarioUpdateRequest ToUpdateRequest()
    {
        return new UsuarioUpdateRequest
        {
            UsuarioId = UsuarioId,
            Nome = Nome,
            Email = Email,
            Senha = Senha
        };
    }
}

public static class UsuarioExtensions
{
    /// <summary>
    /// Converte de Usuário para UsuarioResponse
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns></returns>
    public static UsuarioResponse ToUsuarioResponse(this Usuario usuario)
    {
        return new UsuarioResponse
        {
            UsuarioId = usuario.UsuarioId,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Senha = usuario.Senha
        };
    }
}