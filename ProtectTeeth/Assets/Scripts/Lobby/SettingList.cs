using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingList : MonoBehaviour
{
    public GameObject playerCanvas;
    public void Start()
    {
        SettingCanvas();
    }
    void OnEnable()
    {
        PlayerSetting.Instance.OnPlayerSkillChanged += SettingCanvas;
    }
    void OnDisable()
    {
        PlayerSetting.Instance.OnPlayerSkillChanged -= SettingCanvas;
    }
    private void SettingCanvas()
    {
        if (playerCanvas != null)
        {
            // 자식 객체 순회
            for (int i = 0; i < 5; i++)
            {
                GameObject child = playerCanvas.transform.GetChild(i).gameObject;

                if (i < PlayerSetting.playerskill.Count)
                {
                    var skillObj = PlayerSetting.playerskill[i];
                    var goodSetting = skillObj.GetComponent<GoodSetting>();
                    var tooth = goodSetting.toothinfo;

                    child.SetActive(true);

                    var image = child.transform.GetChild(0).GetComponent<Image>();
                    var sprite = tooth.prefab.GetComponent<SpriteRenderer>().sprite;
                    if (image.sprite != sprite)
                        image.sprite = sprite;

                    var canvasInfo = child.GetComponent<CanvasGetInfo>();
                    if (canvasInfo.thisInfo != tooth.prefab)
                        canvasInfo.thisInfo = tooth.prefab;

                    child.transform.GetChild(1).gameObject.SetActive(true);

                    var tmp = child.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                    string coinText = tooth.coin.ToString();
                    if (tmp.text != coinText)
                        tmp.text = coinText;
                }
                else
                {
                    child.SetActive(false);
                }
            }
        }
    }
}
