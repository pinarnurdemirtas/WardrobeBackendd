using MailKit.Net.Smtp;
using MimeKit;

namespace WardrobeBackendd.Services
{
    public class EmailService
    {
        // Gönderici e-posta bilgileri
        private const string EmailSender = "pncpnc979@gmail.com";
        private const string EmailPassword = "vcrw lerx bgeb upgp";

        // Doğrulama e-postası gönderir
        public async Task SendConfirmationEmail(string userEmail, string verificationUrl, string userDocument)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("DIGITAL WARDROBE", EmailSender));
            mimeMessage.To.Add(new MailboxAddress("Yeni Kullanıcı", userEmail));
            mimeMessage.Subject = "Hesap Doğrulaması Gerekiyor";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"DOĞRULAMA LİNKİ: {verificationUrl}\n{userDocument}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync(EmailSender, EmailPassword);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }

        // Hesap doğrulandı e-postası gönderir
        public async Task SendAccountVerifiedEmail(string userEmail)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("DIGITAL WARDROBE", EmailSender));
            mimeMessage.To.Add(new MailboxAddress("Kullanıcı", userEmail));
            mimeMessage.Subject = "Hesabınız Başarıyla Doğrulandı";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = "Hesabınız başarıyla doğrulandı. Artık sisteme giriş yapabilirsiniz."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync(EmailSender, EmailPassword);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
