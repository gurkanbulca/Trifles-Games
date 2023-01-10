using System;
using GameStateSystem;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [Serializable]
    public class MiniGame
    {
        public GameObject miniGame;
        public GameState gameState;
    }

    [SerializeField] private MiniGame[] miniGames;

    private void Awake()
    {
        GameStateController.OnCurrentGameStateChanged += HandleGameStateChange;
    }

    private void OnDestroy()
    {
        GameStateController.OnCurrentGameStateChanged -= HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState state)
    {
        foreach (var miniGame in miniGames)
        {
            miniGame.miniGame.SetActive(miniGame.gameState == state);
        }
    }
}