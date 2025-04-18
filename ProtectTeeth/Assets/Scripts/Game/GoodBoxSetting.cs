using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoxSetting : MonoBehaviour
{
    public GoodSetting goodSetting;
    bool isAttacking = false;
    public Vector2 boxSize = new Vector2(3f, 1f);
    int layerMask = 1 << 8;
    private Collider2D[] _enemyHits = new Collider2D[20];

    void Update()
    {
        Vector2 boxCenter = transform.position;

        // ���� ���� ���� ����
        int count = Physics2D.OverlapBoxNonAlloc(boxCenter, boxSize, 0f, _enemyHits, layerMask);

        if (count > 0 && !isAttacking)
        {
            isAttacking = true;
            goodSetting.goAttack();
        }
        else if (count == 0 && isAttacking)
        {
            isAttacking = false;
            goodSetting.finishAttack();
        }
    }

    // ����׿� �ð�ȭ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    
}
