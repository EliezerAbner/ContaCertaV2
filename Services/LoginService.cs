using Entities;
using ServicesContracts;
using ServicesContracts.DTO;

namespace Services;

public class LoginService : ILoginService
{
    private readonly ContaCertaDbContext _db;
    
    public LoginService(ContaCertaDbContext db)
    {
        _db = db;
    }
    
    public Task<UsuarioResponse> FazerLogin(string email, string senha)
    {
        throw new NotImplementedException();
    }

    public async Task<UsuarioResponse> NovaConta(UsuarioAddRequest? novoUsuario)
    {
        if (novoUsuario == null)
            throw new ArgumentNullException(nameof(novoUsuario));

        //a fazer: criptografia na senha
        //validação do e-mail
        
        Usuario usuario = novoUsuario.ToUsuario();
        usuario.UsuarioId = Guid.NewGuid();
        
        _db.Add(usuario);
        await _db.SaveChangesAsync();
        
        return usuario.ToUsuarioResponse();
    }

    public async Task<UsuarioResponse> AlterarConta(UsuarioUpdateRequest alterarUsuario)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletarConta(Guid id)
    {
        throw new NotImplementedException();
    }
}