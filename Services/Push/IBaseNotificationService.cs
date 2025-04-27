namespace AutoNotificationService.Services.Push
{
    public interface IBasePushNotificationServices
    {
        Task<ResultCommon> SendInAppNotificationAsync(InAppNotificationModel model);
        Task<ResultCommon> SendPushNotificationAsync(PushNotificationModel model);
        Task<ResultCommon> SendWebNotificationAsync(WebNotificationModel model);
        Task<ResultCommon> SendBulkNotificationAsync(BulkNotificationModel model);
    }

}
