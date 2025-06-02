using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoodAttack : MonoBehaviour
{
    public float speed = 10f; // 공의 속도
    public float lifetime = 5f; // 공의 생존 시간
    public int damage = 10; // 공의 데미지

    void Start()
    {

        // 일정 시간이 지나면 공을 제거
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 공을 앞으로 이동
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bad"))
        {
            // 적에게 데미지를 입히는 로직
            //Debug.Log($"Hit {collider.name}, dealing {damage} damage.");
            MonsterSetting monster = collider.GetComponent<MonsterSetting>();
            monster.startChage();
            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
            Destroy(gameObject); // 공 파괴
        }
    }
    

}
