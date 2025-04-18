using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickStartBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public string changeScenename;
    public string sceneAddress = "Stage1";
    public void Awake()
    {
        this.GetComponent<Button>().interactable = true;
    }
    public void btnclick()
    {
        this.GetComponent<Button>().interactable = false;
        Debug.Log(changeScenename);
        SceneChange.Instance.FadeAndLoadScene(changeScenename, sceneAddress);
    }
}
