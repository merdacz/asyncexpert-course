using System;
using System.Runtime.CompilerServices;

namespace AwaitableExercises.Core
{
    public static class BoolExtensions
    {
        public static BoolAwaiter GetAwaiter(this bool @this)
        {
            return new BoolAwaiter(@this);
        }
    }

    public class BoolAwaiter : INotifyCompletion
    {
        private readonly bool value;

        public BoolAwaiter(bool value)
        {
            this.value = value;
        }

        public bool IsCompleted => false;

        public bool GetResult()
        {
            return this.value;
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }
}
