namespace AutoNotificationService.Notifications;
public class ResultNotifier : ResultCommon
{
    public NotificationType Type { get; set; }


   
}

public enum NotificationType
{
    Email,
    Sms,
    Push,
    EmailAndSms,
    SmsAndPush,
    All,
    WhatsApp 
}

public interface IBaseNotifier
{
    Task<ResultNotifier> NotifyAsyn<T>(T model) where T : class;
}

public interface IProviderNotifier : IBaseNotifier
{
    NotificationType Type { get; }
    bool HasNotifierModel<T>(T model) where T :class;
}







