using System;

namespace NFrameWork.EventManager
{
    [Flags]
    public enum EventPoolMode : byte
    {
        /// <summary>
        /// 默认模式，即必须存在一个事件处理函数
        /// </summary>
        Default = 0,

        /// <summary>
        /// 允许不存在事件处理函数
        /// </summary>
        AllowNoHandler = 1 << 0,

        /// <summary>
        /// 允许存在多个事件处理函数
        /// </summary>
        AllowMultiHandler = 1 << 1,

        /// <summary>
        /// 允许存在重复的事件处理函数
        /// </summary>
        AllowDuplicateHandler = 1 << 2,
    }
}