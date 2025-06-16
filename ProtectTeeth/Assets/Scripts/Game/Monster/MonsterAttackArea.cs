using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackArea : MonoBehaviour
{
    public MonsterSetting monsterSetting;
    bool isAttacking = false;
    public Vector2 boxSize = new Vector2(3f, 1f);
    private Collider2D[] _enemyHits = new Collider2D[1];
    int layerMask = 1 << 6;
    // Update is called once per frame
    void Update()
    {

        Vector2 offset = new Vector2(-boxSize.x * 0.5f, 0); // 왼쪽으로 절반만큼 이동
        Vector2 boxCenter = (Vector2)transform.position + offset;
        int count = Physics2D.OverlapBoxNonAlloc(boxCenter, boxSize, 0f, _enemyHits, layerMask);
       
        if(count>0 && !isAttacking)
        {
            Debug.Log(count);
            isAttacking = true;
            monsterSetting.StartAttack();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
