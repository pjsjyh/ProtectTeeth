using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGame.GoodList;
using TMPro;
public class GamePlayManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerCanvas;

    public void Start()
    {
        SettingCanvas();
    }
    private void SettingCanvas()
    {
        if (playerCanvas != null)
        {
            // �ڽ� ��ü ��ȸ
            for(int i=0;i< PlayerSetting.Instance.playerskill.Count; i++)
            {
                GameObject child = playerCanvas.transform.GetChild(i).gameObject;
                child.transform.GetChild(0).GetComponent<Image>().sprite = PlayerSetting.Instance.playerskill[i].GetComponent<GoodSetting>().toothinfo.prefab.GetComponent<SpriteRenderer>().sprite;
                child.GetComponent<CanvasGetInfo>().thisInfo = PlayerSetting.Instance.playerskill[i].GetComponent<GoodSetting>().toothinfo.prefab;
                child.transform.GetChild(1).gameObject.SetActive(true);
                child.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerSetting.Instance.playerskill[i].GetComponent<GoodSetting>().toothinfo.coin.ToString();
            }
        }
    }
    void SetupChild(GameObject child, int index)
    {
        // �ڽ� ��ü�� ���� ���� ���� �߰�
        Debug.Log($"Setting up child {index}: {child.name}");

        // ����: �ڽ� ��ü Ȱ��ȭ
        child.SetActive(true);
    }
}
