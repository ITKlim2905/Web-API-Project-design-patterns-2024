namespace WebApiOrderFood.BusinessLogic.Mediator;

public interface IMediator
{
    Task Notify(object sender, string eventCode, object data = null);
}
