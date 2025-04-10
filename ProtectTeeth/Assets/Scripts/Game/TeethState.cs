using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethState : MonoBehaviour, IAttackable
{
    public Sprite[] teethbase;
    public float thisHealth = 50;
    private SpriteRenderer spr;
    private GameOver gm;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameOver").GetComponent<GameOver>();
    }
    public void TakeDamage(float damage)
    {
        thisHealth -= damage;
       
        if (thisHealth <= 0)
        {
            Die();
        }
        
        else if (thisHealth <= 15)
        {
            spr.sprite = teethbase[2];

        }
        else if (thisHealth <= 30)
        {
            spr.sprite = teethbase[1];
        }
    }
    void Die()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
        Destroy(this.gameObject);
    }
}
