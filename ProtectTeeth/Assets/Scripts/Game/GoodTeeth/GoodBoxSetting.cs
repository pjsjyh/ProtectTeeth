using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ToothInfos;
public class GoodBoxSetting : MonoBehaviour
{
    public GoodSetting goodSetting;
    bool isAttacking = false;
    public Vector2 boxSize = new Vector2(3f, 1f);
    int layerMask = 1 << 8;
    private Collider2D[] _enemyHits = new Collider2D[20];
    public GameObject effect;
    void Update()
    {
        Vector2 boxCenter = transform.position;

        switch (goodSetting.toothinfo.toothType)
        {
            case ToothEnum.bomb:
                BombAttack();
                break;

            default:
                // 기존 중심 검사 유지
                int count = Physics2D.OverlapBoxNonAlloc(boxCenter, boxSize, 0f, _enemyHits, layerMask);
                BubbleAttack(count);
                break;
        }
    }

    void BubbleAttack(int count)
    {
        if (count > 0 && !isAttacking)
        {
            isAttacking = true;
            goodSetting.GoAttack();
        }
        else if (count == 0 && isAttacking)
        {
            isAttacking = false;
            goodSetting.FinishAttack();
        }
    }
    void BombAttack()
    {
        if (goodSetting.thisHealth <= 0 &&!isAttacking)
        {
            Vector2 origin = transform.position;
            SpawnExplosion(origin);
            Vector2[] directions = new Vector2[]
            {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
            };
            foreach (var dir in directions)
            {
                Vector2 boxCenter = origin + dir;
                SpawnExplosion(boxCenter);
                int count = Physics2D.OverlapBoxNonAlloc(boxCenter, boxSize, 0f, _enemyHits, layerMask);

                for (int i = 0; i < count; i++)
                {
                    var hit = _enemyHits[i];
                    if (hit != null)
                    {
                        var enemy = hit.GetComponent<MonsterSetting>();
                        if (enemy != null)
                        {
                            enemy.TakeDamage(goodSetting.toothinfo.toothBody.attack);
                        }
                    }
                }
            }
            goodSetting.GoAttack();
            isAttacking = true;
        }
    }
    private void SpawnExplosion(Vector2 position)
    {
        if (effect != null)
        {
            Instantiate(effect, position, Quaternion.identity);
        }
    }
    // 디버그용 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    
}
