using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RoundSetting : MonoBehaviour
{
    public GameObject playerCanvas;
    // Start is called before the first frame update
    public void Start()
    {
        SettingCanvas();
    }

    private void SettingCanvas()
    {
        if (playerCanvas != null)
        {
            // 자식 객체 순회
            for (int i = 0; i < 5; i++)
            {
                
                GameObject child = playerCanvas.transform.GetChild(i).gameObject;
                StartGame ri = child.GetComponent<StartGame>();
                ri.thisBigRound = PlayerSetting.bigRound;
                ri.thisSmallRound =  i;
                var tmp = child.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                tmp.text = PlayerSetting.bigRound.ToString()+"-"+(PlayerSetting.bigRound+i).ToString();

                if (i > PlayerSetting.smallRound)
                {
                    child.GetComponent<Button>().interactable = false;

                }
                else
                {
                    child.GetComponent<Button>().interactable = true;

                }
            }
        }
    }
}
