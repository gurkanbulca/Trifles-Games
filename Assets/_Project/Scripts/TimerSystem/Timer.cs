using System;

namespace TimerSystem
{
    public class Timer
    {
        public event Action OnTimerFinish = delegate { };

        private float _timerStart;

        public float timer { get; private set; }

        public Timer(float timer)
        {
            this.timer = timer;
            _timerStart = timer;
        }

        public void DecreaseTimer(float second)
        {
            timer -= second;
            CheckForTimerFinish();
        }

        public void ResetTimer() => timer = _timerStart;

        private void CheckForTimerFinish()
        {
            if (timer <= 0)
                OnTimerFinish();
        }
    }
}