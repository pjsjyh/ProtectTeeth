using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finishWord;
    public void SetGameOver()
    {
        finishWord.text = "Fail";
        Invoke("ClearGame", 0.05f);
    }
    public void SetGameClear()
    {
        finishWord.text = "Clear";
        Invoke("ClearGame", 0.05f);

    }
    private void ClearGame()
    {
        scoreText.text = PlayerSetting.playerScore.ToString();
        transform.GetChild(0).gameObject.SetActive(true);
        ObjectPool.Instance.DeactivateAll();

    }
}
