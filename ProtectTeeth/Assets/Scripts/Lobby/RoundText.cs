using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundText : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = PlayerSetting.MaxRound.bigRound.ToString() + "-" + PlayerSetting.MaxRound.smallRound.ToString();
    }
}
