using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ZombiesScript;
using System;

public class MonsterSetting : MonoBehaviour, IAttackable
{
    public float moveSpeed = 2f;
    private bool isMoving = true, isAttacking = false;
    public Zombie myZombieInfo;
    private Animator animator;
    public float thisHealth;
    public GameObject attackObj;

    private IAttackable target;
    private Coroutine attackRoutine;
    public Action onDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
        moveSpeed = myZombieInfo.zombieBody.speed;
        thisHealth = myZombieInfo.zombieBody.health;

    }
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        var targetMono = target as MonoBehaviour;

        // 완전히 파괴되었거나 null이라면 중지
        if (target == null || (object)target == null || targetMono == null)
        {
            if (myZombieInfo.zombieAttackType != ZombieType.farAttack)
            {
                StopAttack();
            }
            return;

        }

        if (!targetMono.gameObject.activeInHierarchy)
        {
            StopAttack();
        }
    }
    void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
        isAttacking = false;
        isMoving = true;
        animator.SetBool("isAttack", false);
        animator.SetBool("isWalk", true);
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject rootObj = collider.transform.root.gameObject;

        if ((rootObj.CompareTag("Good") || collider.CompareTag("teeth")) && !isAttacking)
        {
            var attackable = collider.GetComponent<IAttackable>();
            if (attackable != null)
            {
                target = attackable;
                StartAttack();
            }
        }
    }
    public void StartAttack()
    {
        Debug.Log("공격 들어옴");
        isMoving = false;
        isAttacking = true;
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", true);

        if (attackRoutine == null && myZombieInfo.zombieAttackType ==ZombieType.nearAttack)
            attackRoutine = StartCoroutine(AttackLoop());
        else if(attackRoutine == null && myZombieInfo.zombieAttackType == ZombieType.farAttack)
        {
            FarAttackLoop();
        }
    }
    IEnumerator AttackLoop()
    {
        while (isAttacking && target != null && ((MonoBehaviour)target).gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(myZombieInfo.zombieBody.speed * 3f);
            target?.TakeDamage(myZombieInfo.zombieBody.attack);
        }

        StopAttack();
    }
    private void  FarAttackLoop()
    {
        InvokeRepeating("FireProjectile", 0.5f, myZombieInfo.zombieBody.attack);

    }
    private void FireProjectile()
    {
        if (attackObj != null)
        {
            // Projectile 인스턴스 생성
            GameObject projectile = Instantiate(attackObj, this.transform.position, this.transform.rotation);
            projectile.GetComponent<GoodAttack>().damage = myZombieInfo.zombieBody.attack;
        }
    }
    public void TakeDamage(float damage)
    {
        thisHealth -= damage;
        Debug.Log(this.name+ "  hp" + thisHealth);
        if (thisHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        StopAttack();
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        sr.color = new Color32(255, 255, 255, 255);
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isDie", true);
        gameObject.SetActive(false);
        thisHealth = myZombieInfo.zombieBody.health;

        onDeath?.Invoke();
        PlayerSetting.Instance.AddScore(myZombieInfo.score);
    }
    public void startChage()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(chageColor());
        }


    }
    public IEnumerator chageColor()
    {

        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer not found on the monster object.");
            yield break;
        }
        sr.color = new Color32(140, 140, 140, 255);
        yield return new WaitForSecondsRealtime(0.02f);
        sr.color = new Color32(255, 255, 255, 255);

    }
}
