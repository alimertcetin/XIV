using XIV.Core.Utils;
using XIV.PoolSystem;

namespace XIV.Core.TweenSystem.OtherTweens
{
    internal sealed class WaitTween : ITween, IPoolable
    {
        IPool pool;
        Timer timer;

        public WaitTween Set(float duration)
        {
            timer.Restart(duration);
            return this;
        }

        void ITween.Update(float deltaTime) => timer.Update(deltaTime);
        bool ITween.IsDone() => timer.IsDone;
        
        void ITween.Complete() => pool?.Return(this);
        void ITween.Cancel() => pool?.Return(this);

        void IPoolable.OnPoolCreate(IPool pool) => this.pool = pool;

        void IPoolable.OnPoolReturn()
        {
            
        }
    }
}