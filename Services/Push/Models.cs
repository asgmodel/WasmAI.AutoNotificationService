namespace AutoNotificationService.Services.Push;
public class InAppNotificationModel
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? ActionUrl { get; set; }


}

public class PushNotificationModel
{
    public string DeviceToken { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public Dictionary<string, object>? Data { get; set; }
}

public class WebNotificationModel
{
    public string SessionId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? ActionLink { get; set; }

    public Dictionary<string, object>? Data { get; set; }

}

public class BulkNotificationModel
{
    public List<string> UserIds { get; set; } = new();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? ActionUrl { get; set; }
}
