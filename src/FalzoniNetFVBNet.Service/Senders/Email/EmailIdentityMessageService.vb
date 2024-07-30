Imports System.Net
Imports System.Net.Mail
Imports FalzoniNetFVBNet.Utils.Keys
Imports Microsoft.AspNet.Identity

Namespace Senders.Email
    Public Class EmailIdentityMessageService
        Implements IIdentityMessageService

        Public Async Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
            Dim client As SmtpClient = New SmtpClient(host:=EmailKeys.EmailHost, port:=EmailKeys.EmailPort) With
        {
            .Credentials = New NetworkCredential(EmailKeys.EmailUserName, EmailKeys.EmailPassword),
            .EnableSsl = True
        }

            Dim from As MailAddress = New MailAddress(EmailKeys.EmailFromAddress, EmailKeys.EmailFromDescription)
            Dim [to] As MailAddress = New MailAddress(message.Destination, message.Destination)

            Dim mailMessage As MailMessage = New MailMessage(from, [to]) With {
            .Subject = message.Subject,
            .Body = message.Body
        }

            Await client.SendMailAsync(mailMessage)
        End Function
    End Class
End Namespace
