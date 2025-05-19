using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBombSetting : GoodSetting
{
    public Collider2D attackCollider;
    public GameObject attackPrefab;
    bool isAttack = false;
    public LayerMask enemyLayer;
    public override void GoAttack()
    {
        if(!isAttack)
            Invoke(nameof(Explode), 0.3f);
        isAttack = true;
    }
    public override void FinishAttack()
    {
        animator.SetBool("isNormal", true);
        animator.SetBool("isAttack", false);
        isAttack = false;
        CancelInvoke("FireProjectile");
    }
    private void FireProjectile()
    {
        if (attackPrefab != null)
        {
            // Projectile 인스턴스 생성
            GameObject projectile = Instantiate(attackPrefab, this.transform.position, this.transform.rotation);
            projectile.GetComponent<GoodAttack>().damage = toothinfo.toothBody.attack;
        }
    }
    private void Explode()
    {
        Die();
    }
}
