namespace WebApiOrderFoodClean.Application.Commands;

public interface INoResponseCommand<in TInput>
{
    void Execute(TInput info);
}
