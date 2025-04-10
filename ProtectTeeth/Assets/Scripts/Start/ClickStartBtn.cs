using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStartBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public string changeScenename;
    public void btnclick()
    {
        Debug.Log(changeScenename);
        SceneChange.Instance.FadeAndLoadScene(changeScenename);
    }
}
