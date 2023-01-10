using System;
using GameStateSystem;
using UnityEngine;
using TimerSystem;

namespace GameCore
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<GameManager>();
                if (_instance) return _instance;

                var go = new GameObject("Game Manager");
                _instance = go.AddComponent<GameManager>();

                return _instance;
            }
        }

        #endregion

        #region Serialized Fields

        [SerializeField] private float timerDuration;
        [SerializeField] private GameState initialGameState;

        #endregion

        #region Private Fields

        private Timer _timer;
        private GameStateController _gameStateController;
        private InputController _inputController;

        #endregion


        #region Unity LifeCycle

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _timer = new Timer(timerDuration);
            _timer.OnTimerFinish += HandleTimerFinish;
            _inputController = new InputController();
            SetTargetFrameRate();
        }

        private void Start()
        {
            _gameStateController = new GameStateController(initialGameState);
        }

        private void OnDestroy()
        {
            _timer.OnTimerFinish += HandleTimerFinish;
        }

        private void Update()
        {
            _timer.DecreaseTimer(Time.deltaTime);
            _inputController.Update(Time.deltaTime);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event handler for Timer's OnTimerFinish action.
        /// Changes games state and reset timer.
        /// </summary>
        private void HandleTimerFinish()
        {
            _gameStateController.currentGameState =
                (GameState) (((int) _gameStateController.currentGameState + 1) %
                             Enum.GetNames(typeof(GameState)).Length);
            _timer.ResetTimer();
        }

        #endregion

        #region Public Methods

        public Vector2 GetDrag() => _inputController.GetDrag();

        public float GetTimer() => _timer.timer;

        #endregion


        #region Helper Methods

        private void SetTargetFrameRate()
        {
            Application.targetFrameRate = 60;
        }

        #endregion
    }
}