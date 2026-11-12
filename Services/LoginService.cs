using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using ServicesContracts.Results;
using ServicesContracts;
using ServicesContracts.DTO;

namespace Services;

public class LoginService : ILoginService
{
    private readonly ContaCertaDbContext _db;
    private readonly ISenhaService _senhaService;
    
    public LoginService(ContaCertaDbContext db, ISenhaService senhaService)
    {
        _db = db;
        _senhaService = senhaService;
    }
    
    public async Task<Result<UsuarioResponse>> FazerLogin(string email, string senha)
    {
        if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            throw new ArgumentNullException(nameof(email), nameof(senha));
        
        Usuario? emailEncontrado = await _db.Usuarios.FirstOrDefaultAsync(temp => temp.Email == email);

        if (emailEncontrado == null)
            return Result<UsuarioResponse>.Falha("E-mail não encontrado");

        if (_senhaService.HashSenha(senha, emailEncontrado.Salt!) == emailEncontrado.Senha)
        {
            return Result<UsuarioResponse>.Ok(emailEncontrado.ToUsuarioResponse());
        }
        else
        {
            return Result<UsuarioResponse>.Falha("Senha incorreta");
        }
    }

    public async Task<Result<UsuarioResponse>> NovaConta(UsuarioAddRequest? novoUsuario)
    {
        if (novoUsuario == null)
            throw new ArgumentNullException(nameof(novoUsuario));
        
        ValidationHelper.ModelValidation(novoUsuario);
        
        Usuario? emailRegistrado = await _db.Usuarios.FirstOrDefaultAsync(temp => temp.Email == novoUsuario.Email);
        
        if (emailRegistrado == null)
            return Result<UsuarioResponse>.Falha("E-mail já registrado");
        
        Usuario usuario = novoUsuario.ToUsuario();
        usuario.UsuarioId = Guid.NewGuid();
        usuario.Salt = Guid.NewGuid().ToString();
        usuario.Senha = _senhaService.HashSenha(usuario.Senha!, usuario.Salt); //ModelValidation já lida com a senha, confia
        
        _db.Add(usuario);
        await _db.SaveChangesAsync();
        
        return Result<UsuarioResponse>.Ok(usuario.ToUsuarioResponse());
    }

    public async Task<UsuarioResponse> AlterarConta(UsuarioUpdateRequest alterarUsuario)
    {
        if (alterarUsuario == null)
            throw new ArgumentNullException(nameof(alterarUsuario));
        
        Usuario? usuarioExiste = await _db.Usuarios.FirstOrDefaultAsync(temp => temp.UsuarioId == alterarUsuario.UsuarioId);

        if (usuarioExiste == null)
            throw new ArgumentException("Usuario não encontrado");
        
        usuarioExiste.Nome = alterarUsuario.Nome;
        usuarioExiste.Email = alterarUsuario.Email;
        usuarioExiste.Senha = alterarUsuario.Senha;
        
        await _db.SaveChangesAsync();
        return usuarioExiste.ToUsuarioResponse();
    }

    public async Task<bool> DeletarConta(Guid? id)
    {
        if(id == null)
            throw new ArgumentNullException(nameof(id));
        
        Usuario? usuarioEncontrado = await _db.Usuarios.FirstOrDefaultAsync(temp => temp.UsuarioId == id);

        if (usuarioEncontrado == null)
            return false;
        
        _db.Remove(_db.Usuarios.First(temp => temp.UsuarioId == id));
        await _db.SaveChangesAsync();
        return true;
    }
}