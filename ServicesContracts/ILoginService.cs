using ServicesContracts.DTO;

namespace ServicesContracts;

/// <summary>
/// Responsável por Criar, editar e deletar contas, além de validar o login
/// </summary>
public interface ILoginService
{
    /// <summary>
    /// Realiza as devidas validações e permite (ou não) o login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="senha"></param>
    /// <returns></returns>
    Task<UsuarioResponse> FazerLogin(string email, string senha);

    /// <summary>
    /// Cria um novo usuário no BD
    /// </summary>
    /// <param name="novoUsuario"></param>
    /// <returns></returns>
    Task<UsuarioResponse> NovaConta(UsuarioAddRequest novoUsuario);
    
    /// <summary>
    /// Edita o usuário no BD
    /// </summary>
    /// <param name="alterarUsuario"></param>
    /// <returns></returns>
    Task<UsuarioResponse> AlterarConta(UsuarioUpdateRequest alterarUsuario);
    
    /// <summary>
    /// Realiza a exclusão do usuário no BD
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna um booleano confirmando (ou não) a exclusão</returns>
    Task<bool> DeletarConta(Guid id);
}