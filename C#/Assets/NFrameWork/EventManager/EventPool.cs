using System;
using System.Collections.Generic;
using NFrameWork.Collection;
using UnityEngine;

namespace NFrameWork.EventManager
{
    public partial class EventPool<T> where T : IEventData
    {
        private readonly MultiMapMpaLink<Type, EventHandler<T>> eventHandlers;
        private readonly Queue<Event> events;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> cachedNodes;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> tempNodes;
        private readonly EventPoolMode eventPoolMode;
        private EventHandler<T> defaultHandler;

        public EventPool(EventPoolMode mode)
        {
            eventHandlers = new MultiMapMpaLink<Type, EventHandler<T>>();
            events = new Queue<Event>();
            cachedNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            tempNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            eventPoolMode = mode;
            defaultHandler = null;
        }

        /// <summary>
        /// 事件处理函数数量
        /// </summary>
        public int EventHandlerCount => eventHandlers.Count;

        /// <summary>
        /// 事件数量
        /// </summary>
        public int EventCount => events.Count;


        /// <summary>
        /// 获取某个事件处理函数数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>()
        {
            if (eventHandlers.TryGetValue(typeof(T), out var range))
            {
                return range.Count;
            }

            return 0;
        }

        /// <summary>
        /// 检查是否存在事件处理函数
        /// </summary>
        /// <param name="handler"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Check(EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new Exception($"Event Handler is Null ,Please Check{typeof(T)} ");
            }

            return eventHandlers.Contains(typeof(T), handler);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="handler"></param>
        /// <exception cref="Exception"></exception>
        public void Subscribe(EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new Exception($"Event Handler is Null,PleaseCheck{typeof(T)}");
            }

            var evtType = typeof(T);
            if (!eventHandlers.Contains(evtType))
            {
                eventHandlers.Add(evtType, handler);
            }
            else if ((eventPoolMode & EventPoolMode.AllowMultiHandler) != EventPoolMode.AllowMultiHandler)
            {
                throw new Exception($"Event {evtType} not allow Multi handle");
            }
            else if ((eventPoolMode & EventPoolMode.AllowDuplicateHandler) != EventPoolMode.AllowDuplicateHandler)
            {
                throw new Exception($"Event {evtType} not allow duplicate Handler");
            }
            else
            {
                eventHandlers.Add(evtType, handler);
            }
        }



        public void UnSubscribe(EventHandler<T> handler)
        {
            
        }


    }
}