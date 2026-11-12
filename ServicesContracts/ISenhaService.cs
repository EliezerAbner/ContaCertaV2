namespace ServicesContracts;

public interface ISenhaService
{
   /// <summary>
   /// Criptografa a senha utilizando o SHA256
   /// </summary>
   /// <param name="senha"></param>
   /// <param name="salt"></param>
   /// <returns></returns>
   public string HashSenha(string senha, string salt);
}