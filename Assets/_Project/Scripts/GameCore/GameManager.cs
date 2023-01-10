using System;
using GameStateSystem;
using UnityEngine;
using TimerSystem;

namespace GameCore
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        [SerializeField] private float timerDuration;
        [SerializeField] private GameState initialGameState;


        private Timer _timer;
        private GameStateController _gameStateController;
        private InputController _inputController;

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

        private void HandleTimerFinish()
        {
            _gameStateController.currentGameState =
                (GameState) (((int) _gameStateController.currentGameState + 1) %
                             Enum.GetNames(typeof(GameState)).Length);
            _timer.ResetTimer();
        }

        public Vector2 GetDrag() => _inputController.GetDrag();

        public float GetTimer() => _timer.timer;

        private void SetTargetFrameRate()
        {
            Application.targetFrameRate = 60;
        }
    }
}