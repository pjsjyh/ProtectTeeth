using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoodAttack : MonoBehaviour
{
    public float speed = 10f; // ���� �ӵ�
    public float lifetime = 5f; // ���� ���� �ð�
    public int damage = 10; // ���� ������

    void Start()
    {

        // ���� �ð��� ������ ���� ����
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // ���� ������ �̵�
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bad"))
        {
            // ������ �������� ������ ����
            //Debug.Log($"Hit {collider.name}, dealing {damage} damage.");
            MonsterSetting monster = collider.GetComponent<MonsterSetting>();
            monster.startChage();
            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
            Destroy(gameObject); // �� �ı�
        }
    }
    

}
