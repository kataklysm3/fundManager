namespace FundManager.Impl
{
    /// <summary>
    /// Service for handling multimpe commands
    /// </summary>
    public interface ICommandsHandlingService
    {
        void Execute(ICommand cmd);
    }
}