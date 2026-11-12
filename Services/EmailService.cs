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
    private readonly ConfigEmail _configEmail;

    public EmailService()
    {
        _configEmail = CarregarConfigEmail();
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
            using (var cliente = new SmtpClient())
            {
                await cliente.ConnectAsync(_configEmail.EnderecoSmtp!, _configEmail.PortaSmtp, SecureSocketOptions.StartTls);

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
    
    private ConfigEmail CarregarConfigEmail()
    {
        string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Configuracao", "configEmail.json"));
        ConfigEmail? config = JsonConvert.DeserializeObject<ConfigEmail>(json);
        
        if (config == null)
            throw new Exception("Erro ao carregar o json de configuração");
        
        return config;
    }
}