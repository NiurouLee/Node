using System;

namespace NFrameWork.EventManager
{
    public interface IEventManager
    {
        int EventHandlerCount { get; }

        int EventCount { get; }

        int Count<T>() where T : IEventData;

        bool Check<T>(EventHandler<T> handler) where T : IEventData;

        void Subscribe<T>(EventHandler<T> handler) where T : IEventData;

        void Unsubscribe<T>(EventHandler<T> handler) where T : IEventData;

        void SetDefaultHandle<T>(EventHandler<T> handler) where T : IEventData;

        void Fire<T>(object sender, T inEventData) where T : IEventData;

        void FireNow<T>(object sender, T InEventData) where T : IEventData;
    }
}