using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public int thisBigRound = 0;
    public int thisSmallRound = 0;

    public bool isStart = false;
    public void startGame()
    {
        if(thisBigRound<=PlayerSetting.bigRound && thisSmallRound <= PlayerSetting.smallRound)
        {
            isStart = true;
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);

        }
    }
}
