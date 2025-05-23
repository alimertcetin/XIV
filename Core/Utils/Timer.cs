﻿namespace XIV.Core.Utils
{
    [System.Serializable]
    public struct Timer
    {
#if UNITY_ENGINE
        [SerializeField]
#endif
        float duration;
        float timer;
        public float NormalizedTime => timer / duration;
        public float NormalizedTimePingPong => NormalizedTime > 0.5f ? (NormalizedTime - 0.5f) / 0.5f :
            NormalizedTime / 0.5f;
        
        public bool IsDone => timer >= duration;
        public float Duration => duration;
        public float PassedTime => timer;
        public float TimeLeft => duration - timer;

        public Timer(float duration)
        {
            this.duration = duration;
            this.timer = 0;
        }

        public bool Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= duration)
            {
                timer = duration;
                return true;
            }
            return false;
        }

        public void Restart()
        {
            timer = 0;
        }

        public void Restart(float newDuration)
        {
            duration = newDuration;
            timer = 0;
        }

        public void ForceComplete()
        {
            timer = duration;
        }

        public override string ToString()
        {
            return $"{timer}/{duration}, NormalizedTime = {NormalizedTime}";
        }
    }
}