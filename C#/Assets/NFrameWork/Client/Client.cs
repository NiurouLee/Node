using System;
using System.Collections.Generic;
using NFrameWork.Singleton;
using NFrameWork.NLog;

namespace NFrameWork
{
    public static class Client
    {
        private static readonly Dictionary<Type, ISingleton> SingletonDic = new Dictionary<Type, ISingleton>();

        private static readonly Stack<ISingleton> Singletons = new Stack<ISingleton>();

        
        

        public static T AddSingleTon<T>() where T : Singleton<T>, new()
        {
            T singleton = new T();
            AddSingleton(singleton);
            return singleton;
        }


        private static void AddSingleton(ISingleton inSingleton)
        {
            Type singletonType = inSingleton.GetType();
            if (SingletonDic.ContainsKey(singletonType))
            {
                Nlog.Err($"Singleton Err:{singletonType.Name}");
                return;
            }

            SingletonDic.Add(singletonType, inSingleton);
            Singletons.Push(inSingleton);
            
            
            
        }
    }
}