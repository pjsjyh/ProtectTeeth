using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoxSetting : MonoBehaviour
{
    public GoodSetting goodSetting;
    bool isAttacking = false;
    HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();
    public Vector2 boxSize = new Vector2(3f, 1f);
    int layerMask = 1 << 8;
    void Update()
    {
        Vector2 boxCenter = transform.position;

        // 범위 안의 몬스터 감지
        Collider2D[] enemies = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, layerMask);
        if (enemies.Length > 0)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                goodSetting.goAttack();
            }
        }
        else
        {

            if (isAttacking)
            {
                isAttacking = false;
                goodSetting.finishAttack();
            }
        }
    }

    // 디버그용 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    
}
