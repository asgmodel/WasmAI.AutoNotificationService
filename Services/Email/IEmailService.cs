
using AutoNotificationService.Services.Base;

namespace AutoNotificationService.Services.Email;

public interface IBaseEmailService: IBaseNotificationService<EmailModel, BatchEmailModel>, ITEmail
{

 

}

