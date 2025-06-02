using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightLight : MonoBehaviour
{
    public Light2D light2D;
    public float turnOnSpeed = 2f;

    private float targetIntensity = 0f;
    private void Start()
    {
        light2D.intensity = 1f;
        if (GameInfo.Instance.isNight)
        {
            StartTurnOff();
        }
    }
    public void StartTurnOff()
    {
        StartCoroutine(TurnOnLight());
    }
    private IEnumerator TurnOnLight()
    {
        while (light2D.intensity > targetIntensity)
        {
            light2D.intensity -= Time.deltaTime * turnOnSpeed;
            yield return null;
        }

        light2D.intensity = targetIntensity;
    }
}
