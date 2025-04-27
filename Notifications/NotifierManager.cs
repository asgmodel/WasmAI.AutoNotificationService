namespace AutoNotificationService.Notifications;
public interface INotifierManager : IBaseNotifier
{
    Task<ResultNotifier> NotifyAsyn<T>(NotificationType type, T model) where T : class;
}
public class NotifierManager : INotifierManager
{
    private readonly IEnumerable<IProviderNotifier> _notifiers;

    public NotificationType Type { get; private set; }

    public NotifierManager(IEnumerable<IProviderNotifier> notifiers)
    {
        _notifiers = notifiers;


    }

  

    public async Task<ResultNotifier> NotifyAsyn<T>(NotificationType type, T model) where T : class
    {
        var notifier = _notifiers.FirstOrDefault(n => n.Type == type);
        if (notifier != null)
        {
            return await notifier.NotifyAsyn(model);
        }

        throw new Exception("Notifier type not found");
    }

    public Task<ResultNotifier> NotifyAsyn<T>(T model) where T : class
    {
        var notifier = _notifiers.FirstOrDefault(x => x.HasNotifierModel(model));
        if(notifier != null) 
             return notifier.NotifyAsyn(model);
        var  res=new ResultNotifier() { IsSuccess=false,Message= "not suppert model in Notifs" };
        return Task.FromResult(res);

    }
}

