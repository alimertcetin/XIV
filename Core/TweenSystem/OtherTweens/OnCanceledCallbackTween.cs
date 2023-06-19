using System;

namespace XIV.Core.TweenSystem.OtherTweens
{
    internal sealed class OnCanceledCallbackTween : CallbackTween
    {
        Action action;

        public OnCanceledCallbackTween Set(Action action)
        {
            this.action = action;
            return this;
        }
        
        protected override void OnComplete()
        {
            
        }

        protected override void OnCanceled()
        {
            action.Invoke();
        }
    }
}