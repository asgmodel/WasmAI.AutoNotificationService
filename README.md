
---

# **AutoNotificationService**

## Overview
`AutoNotificationService` is a versatile notification management library that allows you to send notifications through multiple providers (Email, SMS, Push, WhatsApp, etc.) dynamically. It uses a flexible and extensible approach to interact with different notification services based on the type of notification required.

This library abstracts the complexity of dealing with different notification channels and provides a unified way to send notifications through a variety of channels.

---

## Features
- **Multi-provider Support**: Easily integrates with various notification channels like Email, SMS, Push, and WhatsApp.
- **Dynamic Provider Selection**: Automatically selects the appropriate provider based on the `NotificationType`.
- **Extensible**: You can easily add new notification providers without modifying existing logic.
- **Asynchronous Operations**: Supports asynchronous notification sending to ensure efficient performance.
- **Error Handling**: Provides clear error messages when unsupported notification types or models are requested.

---

## Installation

You can add the `AutoNotificationService` library to your project via NuGet:

```bash
Install-Package WasmAI.AutoNotificationService
```

Alternatively, you can add the package reference in your `.csproj` file:

```xml
<PropertyGroup>
  <PackageId>WasmAI.AutoNotificationService</PackageId>
</PropertyGroup>
```

---

## Getting Started

### Step 1: Register Notification Providers
First, you need to register your notification providers (`EmailNotifier`, `SmsNotifier`, `PushNotifier`, etc.) with the `NotifierManager`. This manager will be responsible for dynamically selecting the appropriate provider.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IEmailNotifier, EmailNotifier>();
    services.AddSingleton<ISmsNotifier, SmsNotifier>();
    services.AddSingleton<IPushNotifier, PushNotifier>();
    services.AddSingleton<INotifierManager, NotifierManager>();
}
```

### Step 2: Send Notifications

Now, you can send notifications by simply specifying the `NotificationType` (e.g., Email, SMS, etc.) and passing the appropriate model.

```csharp
public class NotificationSender
{
    private readonly INotifierManager _notifierManager;

    public NotificationSender(INotifierManager notifierManager)
    {
        _notifierManager = notifierManager;
    }

    public async Task SendNotification()
    {
        var emailModel = new EmailModel
        {
            ToEmail = "user@example.com",
            Subject = "Welcome to our service!",
            Body = "We are glad to have you onboard."
        };

        var result = await _notifierManager.NotifyAsyn(NotificationType.Email, emailModel);
        Console.WriteLine(result.Message);
    }
}
```

In this example, the `NotifierManager` will dynamically select the `EmailNotifier` based on the `NotificationType.Email` provided.

---

## Supported Notification Types

- **Email**: Send notifications via email.
- **SMS**: Send notifications via SMS.
- **Push**: Send notifications via push notifications.
- **WhatsApp**: Send notifications via WhatsApp.
- **EmailAndSms**: Send notifications via both Email and SMS.
- **SmsAndPush**: Send notifications via both SMS and Push.
- **All**: Send notifications via all available channels.

You can easily extend the library to add more providers by implementing the `IProviderNotifier` interface for the new channel.

---

## Classes and Interfaces

### **INotifierManager**
The core interface that defines the functionality of sending notifications across different channels. It includes methods like `NotifyAsyn<T>(NotificationType type, T model)` to dynamically send notifications.

### **IProviderNotifier**
Each notification provider (Email, SMS, Push, etc.) implements this interface to handle its specific notification logic.

### **ResultNotifier**
A class that represents the result of a notification operation. It includes properties such as `IsSuccess` and `Message`.

### **NotificationType**
An enum that defines different notification types (Email, SMS, etc.).

---

## Example Workflow

1. **Initialize the `NotifierManager`** with the providers you want to use.
2. **Call `NotifyAsyn`** with the appropriate `NotificationType` and notification model.
3. The `NotifierManager` will dynamically select the correct provider based on the `NotificationType` and send the notification.

### Example:

```csharp
var emailModel = new EmailModel
{
    ToEmail = "user@example.com",
    Subject = "Your Subject",
    Body = "Your message content."
};

var result = await _notifierManager.NotifyAsyn(NotificationType.Email, emailModel);
```

### Output:

```json
{
    "IsSuccess": true,
    "Message": "Notification sent successfully."
}
```

---

## Extending the Library

To add a new notification provider (e.g., a new type of push notification or a new messaging platform), implement the `IProviderNotifier` interface for the new provider and register it in the `NotifierManager`.

---

## Conclusion

`AutoNotificationService` provides a flexible, extensible, and easy-to-use notification system that can communicate with multiple notification providers in a dynamic way. It enables you to handle notifications through various channels (email, SMS, push notifications, etc.) using a single unified API.

By using this library, you can easily scale your notification system and add new channels as needed without changing the core logic of your application.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

