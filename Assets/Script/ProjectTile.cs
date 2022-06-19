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
            //발사체를 target의 위치로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else  //여러 이유로 target이 사라지면
        {
            //발사체 오브젝트 삭제
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; //적이 아닌 대상과 부딪히면
        if (collision.transform != target) return; //현재  target인 적이 아닐때

        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Destroy(gameObject); //총알 삭제
    }
}
