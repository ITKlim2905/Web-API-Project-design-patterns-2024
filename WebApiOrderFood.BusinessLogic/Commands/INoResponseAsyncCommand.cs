namespace WebApiOrderFood.BusinessLogic.Commands;

public interface INoResponseAsyncCommand<in TInput>
{
    Task Execute(TInput info);
}
