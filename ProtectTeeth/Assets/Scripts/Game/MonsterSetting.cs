using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ZombiesScript;

public class MonsterSetting : MonoBehaviour, IAttackable
{
    public float moveSpeed = 2f;
    private bool isMoving = true, isAttacking = false;
    public Zombie myZombieInfo;
    private Animator animator;
    public float thisHealth;

    private IAttackable target;
    private GoodSetting attackGoodTarget;
    private TeethState attackTeeth;
    private Coroutine attackRoutine;
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
            StopAttack();
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
    void StartAttack()
    {
        isMoving = false;
        isAttacking = true;
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", true);

        if (attackRoutine == null)
            attackRoutine = StartCoroutine(AttackLoop());
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
        StopAttack();
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isDie", true);
        gameObject.SetActive(false);
        PlayerSetting.Instance.AddScore(myZombieInfo.score);
    }
    public void startChage()
    {
        StartCoroutine(chageColor());

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
