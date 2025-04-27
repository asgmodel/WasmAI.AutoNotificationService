



using AutoNotificationService.Services.Email;

namespace AutoNotificationService.Notifications;
public interface IEmailNotifier : IProviderNotifier
{
}
public class EmailNotifier : IEmailNotifier
{
    private readonly IBaseEmailService sender;
    public NotificationType Type => NotificationType.Email;

    public EmailNotifier(IBaseEmailService emailer)
    {
        sender = emailer;
    }
    
    public bool HasNotifierModel<T>(T model) where T : class
    {
        return sender.HasModel(model.GetType());
    }

    public async Task<ResultNotifier> NotifyAsyn<T>(T model) where T : class
    {

        var res = await sender.SendAsync(model);


        return new ResultNotifier { Type = NotificationType.Email,IsSuccess=res.IsSuccess,Message=res.Message};
    }
}

