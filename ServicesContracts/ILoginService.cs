using ServicesContracts.Results;
using ServicesContracts.DTO.Usuario; 

namespace ServicesContracts;

/// <summary>
/// Responsável por Criar, Editar e Deletar contas, além de validar o login
/// </summary>
public interface ILoginService
{
    /// <summary>
    /// Realiza as devidas validações e permite (ou não) o login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="senha"></param>
    /// <returns>Retorna um UsuarioResponse com o resultado (Ok se tudo estiver certo ou Falha caso haja algum erro de validação)</returns>
    Task<Result<UsuarioResponse>> FazerLogin(string email, string senha);

    /// <summary>
    /// Valida o e-mail, criptografa a senha e cadastra o usuário no BD
    /// </summary>
    /// <param name="novoUsuario"></param>
    /// <returns>Retorna um UsuarioResponse com o resultado (Ok se tudo estiver certo ou Falha caso haja algum erro de validação)</returns>
    Task<Result<UsuarioResponse>> NovaConta(UsuarioAddRequest novoUsuario);
    
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
    Task<bool> DeletarConta(Guid? id);
}