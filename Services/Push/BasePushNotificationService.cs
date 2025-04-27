using Microsoft.AspNetCore.Http;
using System.Linq;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AutoNotificationService.Services.Push;
public enum NotificationType
{
    InApp,       
    Push,        
    Web,         
    Bulk          
}

public abstract class BasePushNotificationService 
{
    public abstract Task<ResultCommon> SendInAppNotificationAsync(InAppNotificationModel model);


    public abstract Task<ResultCommon> SendPushNotificationAsync(PushNotificationModel model);


    public abstract Task<ResultCommon> SendWebNotificationAsync(WebNotificationModel model);


    public abstract Task<ResultCommon> SendBulkNotificationAsync(BulkNotificationModel model);
   
}
