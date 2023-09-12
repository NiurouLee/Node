namespace NFrameWork.Module
{
    public class GameModuleBase<T> where T : GameModuleLogicBase
    {
        public T Logic { get; private set; }
    }
}