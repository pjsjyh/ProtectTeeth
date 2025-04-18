using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartLoad : MonoBehaviour
{
    public string sceneAddress = "Stage1";
    // Start is called before the first frame update
    void Start()
    {
        AddressableImageLoader.LoadScene(sceneAddress);
    }

}
