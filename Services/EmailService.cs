using MailKit.Net.Smtp;
using MailKit.Security;
using MailKit;
using MimeKit;
using Newtonsoft.Json;
using ServicesContracts;
using Entities;

namespace Services;

public class EmailService : IEmailService
{
    private readonly string _enderecoSMTP;
    private readonly int _port;

    public EmailService()
    {
        CarregarConfigEmail();
    }
    
    public async Task<bool> EnviarEmail(string remetente, string destinatario, string assunto, string mensagem)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("ContaCerta", remetente));
        email.To.Add(new MailboxAddress("", destinatario));
        email.Subject = assunto;
        email.Body = new TextPart("plain")
        {
            Text = mensagem
        };

        try
        {
            SendResult resultado;
            
            using (var cliente = new SmtpClient())
            {
                await cliente.ConnectAsync(_enderecoSMTP, _port, SecureSocketOptions.StartTls);

                await cliente.SendAsync(email);
                await cliente.DisconnectAsync(true);
            }
        }
        catch(Exception ex)
        {
            return false;
        }

        return true;
    }
    
    private void CarregarConfigEmail()
    {
        try
        {
            string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Configuracao", "configEmail.json"));
            ConfigEmail? config = JsonConvert.DeserializeObject<ConfigEmail>(json);
        }
        catch(Exception ex)
        {
            string msg = "Erro ao carregar o json de configuração: " + ex.Message;
            //tratar erros :/
        }
    }
}