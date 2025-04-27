using System.ComponentModel.DataAnnotations;

namespace AutoNotificationService.Services.Sms;

public  interface ITSmsModel { }
public class SmsModel : BaseNotificationModel, ITSmsModel
{
    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string? ToPhoneNumber { get; set; }
}

public class BatchSmsModel : BaseNotificationModel, ITSmsModel
{
    [Required(ErrorMessage = "At least one phone number is required.")]
    [Phone(ErrorMessage = "One or more phone numbers are invalid.")]
    public List<string>? ToPhoneNumbers { get; set; }
}

