using AutoNotificationService.Services.Sms;

namespace AutoNotificationService.Notifications;
public interface ISmsNotifier : IProviderNotifier
{
}
public class SmsNotifier : ISmsNotifier
{

    private readonly IBaseSmsService sender;
    public SmsNotifier(IBaseSmsService smsService)
    {
        sender = smsService;
    }

    public NotificationType Type => NotificationType.Sms;

    public bool HasNotifierModel<T>(T model) where T : class
    {
        // تحقق من وجود النموذج
        return sender.HasModel(model.GetType());
    }

    public async Task<ResultNotifier> NotifyAsyn<T>(T model) where T : class
    {
        return new ResultNotifier { Type = NotificationType.Sms };
    }
}

