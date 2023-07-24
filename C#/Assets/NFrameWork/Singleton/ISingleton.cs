namespace NFrameWork.Singleton
{
    public interface ISingleton
    {
        bool IsDisposed();

        void Register();

        void Destroy();
    }
}