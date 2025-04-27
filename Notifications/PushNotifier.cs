namespace AutoNotificationService.Notifications;
public interface IPushNotifier : IProviderNotifier
{
}
public class PushNotifier : IPushNotifier
{
    public NotificationType Type => NotificationType.Push;



    public bool HasNotifierModel<T>(T model) where T : class
    {
        return true;
    }

    public async Task<ResultNotifier> NotifyAsyn<T>(T model) where T : class
    {


        return new ResultNotifier { Type = NotificationType.Push };
    }
}

