namespace WebApiOrderFood.BusinessLogic.Commands;

public interface INoResponseCommand<in TInput>
{
    void Execute(TInput info);
}
