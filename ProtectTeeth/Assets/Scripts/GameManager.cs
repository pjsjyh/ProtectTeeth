using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerSetting playerSkill;
    // ���� ����
    public enum GameState { Login, MainMenu, Playing, Paused, GameOver, RoundClear }
    public GameState CurrentState { get; private set; }
    public int StageBig = 0;
    public int StageSmall = 0;
    void Awake()
    {
        // Singleton ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
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
