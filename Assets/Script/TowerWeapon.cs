using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // �߻�ü ������
    [SerializeField]
    private Transform spawnPoint; // �߻�ü ������ġ
    [SerializeField]
    private float attackRate = 0.5f; // ���� �ӵ�
    [SerializeField]
    private float attackRange = 2.0f; // ���ݹ���
    [SerializeField]
    private int attackDamage = 1;

    private WeaponState weaponState= WeaponState.SearchTarget; // Ÿ�� ������ ����
    private Transform attackTarget = null; //���ݴ��
    private Spawner enemySpawner; // ���ӿ� �����ϴ� �� ���� ȹ���


    public void Setup(Spawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        //���� ���¸� waponState.SearchTarget���� ����
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        //������ ������̴� ���� ����
        StopCoroutine(weaponState.ToString());

        //���º���
        weaponState = newState;

        //���ο� ���� ���
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    //Ÿ���� ���� �ٶ󺸰� �ϴ� �Լ�
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        //x,y���������� �������ϱ� ������ radian�����̱� ������ rad2deg�� ���� �� ������ ����
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {

            //���� ������ �ִ� �Ÿ� ����
            float closestDistSqr = Mathf.Infinity;

            //������ ��������Ʈ�� �ϳ��� �޾ƿͼ� �ϳ��� �˻���
            for(int i =0; i< enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                //���� �˻����� ������ �Ÿ��� ���ݹ������� �ְ�, ������� �˻��� ������ �Ÿ��� ������ 
                if (distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            if(attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while(true)
        {
            //target�� �ִ��� �˻�(�ٸ� �߻�ü�� ���� ����, Goal �������� �̵��ؼ� �����Ǵ� ��� ��)
            if(attackTarget== null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //target�� ���� ���� �ȿ� �ִ��� �˻�(���� ������ ����� ���ο� �� Ž��)
            float distance = Vector3.Distance(attackTarget.transform.position, transform.position);

            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate);

            //����(�߻�ü ����)
            SpawnProjectile();
        }

    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<ProjectTile>().Setup(attackTarget, attackDamage);
    }
}
