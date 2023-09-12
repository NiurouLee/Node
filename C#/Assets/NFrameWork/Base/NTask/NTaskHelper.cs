using System;
using System.Collections.Generic;


namespace NFrameWork.NTask
{
    public static class NTaskHelper
    {
        public static bool IsCancel(this NCancellationToken self)
        {
            if (self == null)
            {
                return false;
            }

            return self.IsDispose();
        }


        private class CoroutineBlocker
        {
            private int count;

            private NTask tcs;

            public CoroutineBlocker(int count)
            {
                this.count = count;
            }

            public async NTask RunSubCoroutineAsync(NTask task)
            {
                try
                {
                    await task;
                }
                catch (Exception e)
                {
                    --this.count;
                    if (this.count <= 0 && this.tcs != null)
                    {
                        NTask t = this.tcs;
                        this.tcs = null;
                        t.SetResult();
                    }
                }
            }

            public async NTask WaitAsync()
            {
                if (this.count <= 0)
                {
                    return;
                }

                this.tcs = NTask.Create();
                await tcs;
            }
        }

        public static async NTask WaitAny(List<NTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(1);

            foreach (var task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }


        public static async NTask WaitAny(NTask[] tasks)
        {
            if (tasks.Length == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(1);

            foreach (var task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async NTask WaitAll(NTask[] tasks)
        {
            if (tasks.Length == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Length);

            foreach (var task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async NTask WaitAll(List<NTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Count);
            foreach (var task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }
    }
}