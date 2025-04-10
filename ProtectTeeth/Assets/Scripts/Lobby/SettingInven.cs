using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingInven : MonoBehaviour
{
    public Transform gridParent;
    public GameObject Item;
    public void Start()
    {
        SettingCanvas();

    }
  
    private void SettingCanvas()
    {
        var skills = PlayerSetting.Instance.nowSettingPlayerSkills;

        for (int i = 0; i < skills.Count; i++)
        {
            var skillObj = skills[i];

            GameObject slot = Instantiate(Item, gridParent);

            // 아이콘 이미지
            Sprite icon = skillObj.GetComponent<GoodSetting>()
                                  .toothinfo.prefab.GetComponent<SpriteRenderer>().sprite;

            slot.GetComponent<Image>().sprite = icon;

            // 가격 표시
            int coin = skillObj.GetComponent<GoodSetting>().toothinfo.coin;
            slot.transform.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coin.ToString();

            // 필요시 정보 저장
            slot.GetComponent<CanvasGetInfo>().thisInfo = skillObj.GetComponent<GoodSetting>().toothinfo.prefab;
        }
    }
}
