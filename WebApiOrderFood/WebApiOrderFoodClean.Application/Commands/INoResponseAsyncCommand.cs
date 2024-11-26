namespace WebApiOrderFoodClean.Application.Commands;

public interface INoResponseAsyncCommand<in TInput>
{
    Task Execute(TInput info);
}
