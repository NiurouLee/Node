namespace NFrameWork.NTask
{
    public enum AwaiterStatus : byte
    {
        Pending = 0,

        Succeeded = 1,

        Faulted = 2,
    }
}