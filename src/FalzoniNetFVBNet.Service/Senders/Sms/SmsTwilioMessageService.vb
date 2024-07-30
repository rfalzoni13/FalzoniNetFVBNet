Imports FalzoniNetFVBNet.Utils.Keys
Imports Microsoft.AspNet.Identity
Imports Twilio
Imports Twilio.Rest.Api.V2010.Account

Namespace Senders.Email
    Public Class SmsTwilioMessageService
        Implements IIdentityMessageService

        Public Async Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
            TwilioClient.Init(TwilioKeys.SMSAccountIdentification, TwilioKeys.SMSAccountPassword)

            Dim smsMessage = Await Task.FromResult(MessageResource.Create(
                body:=message.Body,
                from:=New Twilio.Types.PhoneNumber(TwilioKeys.SMSAccountFrom),
                [to]:=New Twilio.Types.PhoneNumber(message.Destination)
            ))

            Trace.TraceInformation(smsMessage.Sid)
        End Function
    End Class

End Namespace
