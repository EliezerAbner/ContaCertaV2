using ServicesContracts.Results;

namespace ServicesContracts;

public interface IEmailService
{
    /// <summary>
    /// Responsável por enviar o e-mail contendo uma mensagem e o arquivo XLMS gerado
    /// </summary>
    /// <param name="remetente"></param>
    /// <param name="destinatario"></param>
    /// <param name="assunto"></param>
    /// <param name="mensagem"></param>
    /// <returns>Retorna um booleano indicando sucesso (ou não)</returns>
    Task<bool> EnviarEmail(string remetente, string destinatario, string assunto, string mensagem);
}