using System;

namespace XIV.Core.TweenSystem.OtherTweens
{
    internal sealed class OnCompleteCallbackTween : CallbackTween
    {
        Action action;

        public OnCompleteCallbackTween Set(Action action)
        {
            this.action = action;
            return this;
        }
        
        protected override void OnComplete() => action.Invoke();
        protected override void OnCanceled() { }
    }
    
    internal sealed class OnCompleteCallbackTween<T> : CallbackTween
    {
        Action<T> action;
        T value;
        
        public OnCompleteCallbackTween<T> Set(Action<T> action, T value)
        {
            this.action = action;
            this.value = value;
            return this;
        }
        
        protected override void OnComplete() => action.Invoke(value);
        protected override void OnCanceled() { }
    }
}