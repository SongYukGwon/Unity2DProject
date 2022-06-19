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
        // ���� ü���� damage��ŭ �����ؼ� ���� ��Ȳ�� �� ������ Ÿ���� ������ ���ÿ� ������
        // die�Լ��� ������ ����� �� ����. ��ٿ�� �ʿ�

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

        //���� ����
        color.a = 0.4f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
