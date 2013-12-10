namespace ICE.MasterReceivers
{
    public interface IMasterCommandReceiver
    {
        string ExecutingCommand { get; }
        void NewCommand(string[] args);
    }
}