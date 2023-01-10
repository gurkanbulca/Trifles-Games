using System;

namespace TimerSystem
{
    public class Timer
    {
        #region Actions

        public event Action OnTimerFinish = delegate { };

        #endregion

        #region Private Fields

        private readonly float _timerStart;

        #endregion

        #region Properties

        public float timer { get; private set; }

        #endregion


        #region Constructor

        public Timer(float timer)
        {
            this.timer = timer;
            _timerStart = timer;
        }

        #endregion

        #region Public Methods

        public void DecreaseTimer(float second)
        {
            timer -= second;
            CheckForTimerFinish();
        }

        public void ResetTimer() => timer = _timerStart;

        #endregion

        #region Helper Methods

        private void CheckForTimerFinish()
        {
            if (timer <= 0)
                OnTimerFinish();
        }

        #endregion
    }
}