using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ToothInfos;
using MyGame.GoodList;
using System;

public class PlayerSetting : MonoBehaviour
{
    public static PlayerSetting Instance { get; private set; }
    public GoodCollection goodList;
    public static List<GameObject> playerskill; // 현재 장착한 5개의 인벤토리
    public static List<GameObject> nowSettingPlayerSkills; //가지고 있는 스킬 목록들
    public static volatile int playerScore = 30;
    public static int bigRound = 0;
    public static int smallRound = 0;


    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;
    public Action OnPlayerSkillChanged;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeInventory()
    {
        playerskill = new List<GameObject>();
        nowSettingPlayerSkills = new List<GameObject>();
        //AddItem(playerskill, goodList.goodPrefab[0]);
        AddItem(nowSettingPlayerSkills, goodList.goodPrefab[0]);
        //AddItem(playerskill, goodList.goodPrefab[1]);
        AddItem(nowSettingPlayerSkills, goodList.goodPrefab[1]);
        //AddItem(playerskill, goodList.goodPrefab[2]);
        AddItem(nowSettingPlayerSkills, goodList.goodPrefab[2]);
    }

    public void AddItem(List<GameObject> list, GameObject item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);

            if (list == playerskill)
            {
                OnPlayerSkillChanged?.Invoke();
            }
        }
    }

    public void RemoveItem(GameObject item)
    {
        if (playerskill.Contains(item))
        {
            playerskill.Remove(item);
            Debug.Log($"{item} removed from inventory.");
        }
        else
        {
            Debug.Log($"{item} not found in inventory.");
        }
        OnPlayerSkillChanged?.Invoke();
    }

    public List<GameObject> GetInventory()
    {
        return playerskill;
    }
    
    public void AddScore(int value)
    {
        Debug.Log(value);
        playerScore += value;
        OnScoreChanged?.Invoke(playerScore);
    }
    public void SubScore(int value)
    {
        playerScore -= value;
        OnScoreChanged?.Invoke(playerScore);
    }
}
