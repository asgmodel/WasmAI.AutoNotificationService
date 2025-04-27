using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AutoNotificationService.Services.Email;

public  interface ITEmailModel
{

}
public class BaseEmailModel : BaseNotificationModel, ITEmailModel
{
    public IList<IFormFile>? Attachments { get; set; }
}

public class EmailModel : BaseEmailModel, ITEmailModel
{
    [Required(ErrorMessage = "Recipient email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? ToEmail { get; set; }

    
}


public class BatchEmailModel : BaseEmailModel, ITEmailModel
{
    [Required(ErrorMessage = "At least one recipient email is required.")]
    [EmailAddress(ErrorMessage = "One or more recipients have an invalid email address.")]
    public List<string>? ToEmails { get; set; }


    public static implicit operator BatchEmailModel(EmailModel model)
    {

        return new BatchEmailModel()
        {
            Attachments = model.Attachments,
            Body = model.Body,
            Subject = model.Subject,
            ToEmails = new() { model.ToEmail }
        };

    }
}
public class EmailTemplateModel : EmailModel, ITEmailModel
{

    public string? TemplateName { get; set; } = string.Empty;
    public Dictionary<string, string>? Placeholders { get; set; }

}


//    هنا يمكن   انشاء  كل قوالب    العرض للايميل او تمريريها  


