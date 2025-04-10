using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    public void SetGameOver()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        ObjectPool.Instance.DeactivateAll();
    }

}
