using AutoNotificationService.Services.Base;
using AutoNotificationService.Services.Email;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Linq;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AutoNotificationService.Services.Sms;

    public interface IBaseSmsConfiguration
    {
        string? AccountSid { get; }
        string? AuthToken { get; }
        string? FromPhoneNumber { get; }
    }

    public class BaseSmsConfiguration : IBaseSmsConfiguration
    {
        public string? AccountSid { get; set; }
        public string? AuthToken { get; set; }
        public string? FromPhoneNumber { get; set; }
        public string? NameApp {  get; set; }
    }

    public class BaseSmsService : BaseNotificationService<ITSmsModel, CreateMessageOptions, SmsModel, BatchSmsModel>,IBaseSmsService
    {
        private readonly IBaseSmsConfiguration _config;

        public BaseSmsService(IBaseSmsConfiguration config)
        {
            _config = config;
            TwilioClient.Init(_config.AccountSid, _config.AuthToken); 
        }

    protected override CreateMessageOptions MapTo<T>(T data)
    {

        if (data is SmsModel model)
            return new CreateMessageOptions(new PhoneNumber(model.ToPhoneNumber))
            {
                From = new PhoneNumber(_config.FromPhoneNumber),
                Body = model.Body
            };

       throw new NotImplementedException();

    }



    protected override async Task<ResultCommon> SendTo(CreateMessageOptions data)
    {
        try
        {
           

            var sentMessage = await MessageResource.CreateAsync(data);
            return ResultCommon.Success($"Message sent to {data.To.ToString()}: {sentMessage.Sid}");
        }
        catch (TwilioException twilioEx)
        {
            return ResultCommon.Failure($"Twilio SMS error: {twilioEx.Message}, Code: {twilioEx.GetHashCode}");
        }
        catch (Exception ex)
        {
            return ResultCommon.Failure($"General error: {ex.Message}");
        }
    }

   

}

   