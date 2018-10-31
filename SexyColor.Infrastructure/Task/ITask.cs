namespace SexyColor.Infrastructure
{
    public interface ITask
    {
        void Execute(TaskInfo task = null);
    }
}
