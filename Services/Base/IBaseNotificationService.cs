namespace AutoNotificationService.Services.Base;

public interface IBaseNotificationService<TModel,TBatch> 
  
    
{
    
    Task<ResultCommon> SendModelAsync(TModel model);
    Task<ResultCommon> SendBatchAsync(TBatch model);
    bool HasModel(Type type);
    Task<ResultCommon> SendAsync<TNotificationModel>(TNotificationModel model) where TNotificationModel : class;

    Task<ResultCommon> SendTemplatedAsync<TTemplate>(TTemplate model) where TTemplate : class;
}

