using AutoNotificationService.Services.Base;

namespace AutoNotificationService.Services.Sms
{
    public interface IBaseSmsService: IBaseNotificationService<SmsModel,BatchSmsModel>, ITSms
    {
      
    }
}
