using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private Movement movement2D;
    private Transform target;
    private int damage;
    public void Setup(Transform target, int damage)
    {
        movement2D = GetComponent<Movement>();
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if(target != null)
        {
            //�߻�ü�� target�� ��ġ�� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else  //���� ������ target�� �������
        {
            //�߻�ü ������Ʈ ����
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; //���� �ƴ� ���� �ε�����
        if (collision.transform != target) return; //����  target�� ���� �ƴҶ�

        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Destroy(gameObject); //�Ѿ� ����
    }
}
