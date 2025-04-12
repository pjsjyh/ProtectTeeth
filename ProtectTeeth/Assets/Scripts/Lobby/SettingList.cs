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
            for (int i = 0; i < PlayerSetting.playerskill.Count; i++)
            {
                GameObject child = playerCanvas.transform.GetChild(i).gameObject;
                child.SetActive(true);
                child.transform.GetChild(0).GetComponent<Image>().sprite = PlayerSetting.playerskill[i].GetComponent<GoodSetting>().toothinfo.prefab.GetComponent<SpriteRenderer>().sprite;
                child.GetComponent<CanvasGetInfo>().thisInfo = PlayerSetting.playerskill[i].GetComponent<GoodSetting>().toothinfo.prefab;
                child.transform.GetChild(1).gameObject.SetActive(true);
                child.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerSetting.playerskill[i].GetComponent<GoodSetting>().toothinfo.coin.ToString();
            }
            for (int i = PlayerSetting.playerskill.Count; i < 5; i++)
            {
                GameObject child = playerCanvas.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
        }
    }
}
