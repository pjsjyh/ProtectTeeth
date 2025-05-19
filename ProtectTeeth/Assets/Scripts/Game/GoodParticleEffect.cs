using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodParticleEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f); // 파티클 길이에 맞게 시간 조정
    }
}
