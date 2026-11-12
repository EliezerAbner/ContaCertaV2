using System.Security.Cryptography;
using System.Text;
using ServicesContracts;

namespace Services;

public class SenhaService : ISenhaService
{
    public string HashSenha(string senha, string salt)
    {
        SHA256 hash = SHA256.Create();
        string senhaToHash = $"{senha}{salt}";
        var bytes = Encoding.Default.GetBytes(senhaToHash);
        
        var senhaHashed = hash.ComputeHash(bytes);
        return Convert.ToHexString(senhaHashed);
    }
}