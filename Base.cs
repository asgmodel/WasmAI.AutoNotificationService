using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AutoNotificationService
{
    public class ResultCommon
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public static ResultCommon Success(string? message = null)
        {
            return new ResultCommon { IsSuccess = true, Message = message };
        }

        public static ResultCommon Failure(string message)
        {
            return new ResultCommon { IsSuccess = false, Message = message };
        }
    }



    public enum NotificationChannel
    {
        Email,        
        Sms,          
        InApp        
    }


    public interface ITEmail
    {

    }
    public  interface INotificationService
    {

    }
    public interface ITSms { }
    public interface ITWeb { }

    public class BaseNotificationModel
    {
        [Required(ErrorMessage = "Subject is required.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        public string? Body { get; set; }
    }

    public class NotificationModel : BaseNotificationModel
    {
        [Required(ErrorMessage = "Recipient To is required.")]
        public string? To { get; set; }
    }

    public class BatchNotificationModel : BaseNotificationModel
    {
         [Required(ErrorMessage = "At least one recipient  is required.")]
          public List<string>? To { get; set; }
    }



    
 
}
