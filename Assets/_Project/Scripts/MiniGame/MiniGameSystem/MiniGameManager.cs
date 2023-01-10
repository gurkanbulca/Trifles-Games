using System;
using GameStateSystem;
using UnityEngine;

namespace MiniGameSystem
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Serializables

        [Serializable]
        public class MiniGame
        {
            public GameObject miniGame;
            public GameState gameState;
        }

        #endregion

        #region Serialized Fields

        [SerializeField] private MiniGame[] miniGames;

        #endregion

        #region Unity LifeCycle

        private void Awake()
        {
            GameStateController.OnCurrentGameStateChanged += HandleGameStateChange;
        }

        private void OnDestroy()
        {
            GameStateController.OnCurrentGameStateChanged -= HandleGameStateChange;
        }

        #endregion


        #region Event Handlers

        /// <summary>
        /// Sets mini games status by current game state.
        /// </summary>
        /// <param name="state"></param>
        private void HandleGameStateChange(GameState state)
        {
            foreach (var miniGame in miniGames)
            {
                miniGame.miniGame.SetActive(miniGame.gameState == state);
            }
        }

        #endregion
    }
}