using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ToothInfos;
public abstract class GoodSetting : MonoBehaviour, IAttackable, TeethIntertace
{
    public ToothInfo toothinfo;
    protected Animator animator;
    public volatile float thisHealth;
    public event System.Action<float, float> OnHealthChanged;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        thisHealth = toothinfo.toothBody.health;
    }

    public virtual void TakeDamage(float damage)
    {
        thisHealth -= damage;
        OnHealthChanged?.Invoke(thisHealth, toothinfo.toothBody.health);
        if (thisHealth <= 0)
        {
            if(toothinfo.toothType!=ToothEnum.bomb)
                Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public abstract void GoAttack();
    public abstract void FinishAttack();
}
