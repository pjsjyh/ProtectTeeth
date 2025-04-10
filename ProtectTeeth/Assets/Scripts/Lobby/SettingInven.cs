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

            // ������ �̹���
            Sprite icon = skillObj.GetComponent<GoodSetting>()
                                  .toothinfo.prefab.GetComponent<SpriteRenderer>().sprite;

            slot.GetComponent<Image>().sprite = icon;

            // ���� ǥ��
            int coin = skillObj.GetComponent<GoodSetting>().toothinfo.coin;
            slot.transform.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coin.ToString();

            // �ʿ�� ���� ����
            slot.GetComponent<CanvasGetInfo>().thisInfo = skillObj.GetComponent<GoodSetting>().toothinfo.prefab;
        }
    }
}
