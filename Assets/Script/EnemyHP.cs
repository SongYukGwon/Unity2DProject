using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHp;
    private bool isDie = false;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHp => maxHP;
    public float CurrentHp => currentHp;
    
    private void Awake()
    {
        currentHp = maxHP;
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // 적의 체력이 damage만큼 감소해서 죽을 상황일 때 여러번 타워의 공격을 동시에 받으면
        // die함수가 여러번 실행될 수 있음. 디바운싱 필요

        if (isDie == true) return;

        currentHp -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if(currentHp <= 0)
        {
            isDie = true;
            enemy.OnDie(EnemyDestoryType.Kill);
        }

    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        //투명도 조절
        color.a = 0.4f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
