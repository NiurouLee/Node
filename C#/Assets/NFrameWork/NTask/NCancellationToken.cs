using System;
using System.Collections.Generic;

namespace NFrameWork.NTask
{
    public class NCancellationToken
    {
        private HashSet<Action> actions = new HashSet<Action>();


        public void Add(Action callback)
        {
            this.actions.Add(callback);
        }


        public void Remove(Action callback)
        {
            this.actions?.Remove(callback);
        }


        public bool IsDispose()
        {
            return this.actions == null;
        }

        public void Cancel()
        {
            if (this.actions == null)
            {
                return;
            }

            this.Invoke();
        }

        private void Invoke()
        {
            HashSet<Action> runActions = this.actions;
            try
            {
                foreach (var action in runActions)
                {
                    action.Invoke();
                }
            }
            catch (Exception e)
            {
                NTask.ExceptionHandler.Invoke(e);
            }
        }
    }
}