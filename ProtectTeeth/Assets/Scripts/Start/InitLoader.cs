using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum thisType
{
    Image,
    RawImage,
}
public class InitLoader : MonoBehaviour
{
    public thisType type;
    public Image myImage;
    public RawImage rawImage;
    public string address;
    void Start()
    {
        if (type == thisType.Image)
        {
            AddressableImageLoader.SetImageFromAddress(myImage, address);

        }
        else if (type == thisType.RawImage)
        {
            AddressableImageLoader.SetRawImageFromAddress(rawImage, address);

        }
    }

}

