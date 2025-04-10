using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GetScore : MonoBehaviour
{
    TextMeshProUGUI score;
    public int getScore=0;
    // Start is called before the first frame update
    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
        getScore = GameObject.Find("PlayerSetting").GetComponent<PlayerSetting>().playerScore;
        PlayerSetting.Instance.OnScoreChanged += UpdateScore;
    }
    // Update is called once per frame
    void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();
    }
}
