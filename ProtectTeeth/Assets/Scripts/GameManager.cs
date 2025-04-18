using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerSetting playerSkill;
    // 게임 상태
    public enum GameState { Login, MainMenu, Playing, Paused, GameOver, RoundClear }
    public GameState CurrentState { get; private set; }
    public int StageBig = 0;
    public int StageSmall = 0;
    void Awake()
    {
        // Singleton 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }

    void Start()
    {
        ChangeState(GameState.Login);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.Login:
                break;
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                GameInfo.Instance.StartGame();

                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                GameOver gameOver = FindObjectOfType<GameOver>();
                gameOver.SetGameOver();
                break;
            case GameState.RoundClear:
                GameOver gameClear = FindObjectOfType<GameOver>();
                gameClear.SetGameClear();
                break;
        }
    }



}
