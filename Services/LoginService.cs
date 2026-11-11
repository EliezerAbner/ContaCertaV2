using ServicesContracts;
using ServicesContracts.DTO;

namespace Services;

public class LoginService : ILoginService
{
    public Task<UsuarioResponse> FazerLogin(string email, string senha)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioResponse> NovaConta(UsuarioAddRequest novoUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioResponse> AlterarConta(UsuarioUpdateRequest alterarUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletarConta(Guid id)
    {
        throw new NotImplementedException();
    }
}