

namespace AutoNotificationService.Services.Base;

public abstract class BaseNotificationService<TNotification, TDataSender, TModel, TBatch> : IBaseNotificationService<TModel, TBatch>
    where TNotification : class
   
    where TModel : class
    where TBatch : class



{

    protected abstract Task<ResultCommon> SendTo(TDataSender data) ;

    protected abstract TDataSender MapTo<T>(T data) where T : class;
    public  bool HasModel(Type type)
    {

        if (typeof(TNotification).IsAssignableFrom(type) )
            return true;
        return false;
    }
   
    public virtual Task<ResultCommon> SendAsync<TNotificationModel>(TNotificationModel model) where TNotificationModel : class
    {
        if (!HasModel(model.GetType()))

            return Task.FromResult(ResultCommon.Failure("no  support "));
        if (model is TModel tmodel)
            return SendModelAsync(tmodel);
        else if(model is TBatch tbatch)
            return SendBatchAsync(tbatch);

        
        return SendTemplatedAsync(model);


    }

    public Task<ResultCommon> SendModelAsync(TModel model)
    {
        var msg = MapTo(model);
        return SendTo(msg);
    }

    public Task<ResultCommon> SendBatchAsync(TBatch model)
    {
        var msg = MapTo(model);
        return SendTo(msg);
    }

    public Task<ResultCommon> SendTemplatedAsync<TTemplate>(TTemplate model) where TTemplate : class
    {
        var msg = MapTo(model);
        return SendTo(msg);
    }
}

