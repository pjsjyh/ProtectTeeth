using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isIn = false;
  
    public void AddThisItem()
    {
        if (isIn)
        {
            if (PlayerSetting.playerskill.Count >= 0)
            {
                GameObject thisItem = GetComponent<CanvasGetInfo>().thisInfo;
                PlayerSetting.Instance.RemoveItem(thisItem);
                isIn = false;
            }
        }
        else
        {
            if (PlayerSetting.playerskill.Count < 5)
            {
                GameObject thisItem = GetComponent<CanvasGetInfo>().thisInfo;
                PlayerSetting.Instance.AddItem(PlayerSetting.playerskill, thisItem);
                isIn = true;
            }
        }
       
    }
   
}
