using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ToothInfos;
public class GoodSetting : MonoBehaviour, IAttackable
{
    public ToothInfo toothinfo;
    public Collider2D attackCollider;
    private Animator animator;
    public volatile float thisHealth;
    public GameObject attackPrefab;
    bool isAttack = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        thisHealth = toothinfo.toothBody.health;
    }

    public void goAttack()
    {
        if (isAttack) return;
        if (animator)
        {
            animator.SetBool("isNormal", false);
            animator.SetBool("isAttack", true);

            isAttack = true;

        }
        isAttack = true;
        InvokeRepeating("FireProjectile", 0.5f, toothinfo.toothBody.speed);

    }
    public void finishAttack()
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
    public void TakeDamage(float damage)
    {
        thisHealth -= damage;
        if (thisHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        
        Destroy(this.gameObject);
    }
}
