using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // 발사체 프리팹
    [SerializeField]
    private Transform spawnPoint; // 발사체 생성위치
    [SerializeField]
    private float attackRate = 0.5f; // 공격 속도
    [SerializeField]
    private float attackRange = 2.0f; // 공격범위
    [SerializeField]
    private int attackDamage = 1;

    private WeaponState weaponState= WeaponState.SearchTarget; // 타워 무기의 상태
    private Transform attackTarget = null; //공격대상
    private Spawner enemySpawner; // 게임에 존재하는 적 정보 획득용


    public void Setup(Spawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        //최초 상태를 waponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        //이전에 재생중이던 상태 종료
        StopCoroutine(weaponState.ToString());

        //상태변경
        weaponState = newState;

        //새로운 상태 재생
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    //타워가 적을 바라보게 하는 함수
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        //x,y변위값으로 각도구하기 각도가 radian단위이기 때문에 rad2deg를 곱해 도 단위를 구함
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {

            //가장 가까이 있는 거리 변수
            float closestDistSqr = Mathf.Infinity;

            //적들의 정보리스트를 하나씩 받아와서 하나씩 검사함
            for(int i =0; i< enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                //현재 검사중인 적과의 거리가 공격범위내에 있고, 현재까지 검사한 적보다 거리가 가까우면 
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
            //target이 있는지 검사(다른 발사체에 의해 제거, Goal 지점까지 이동해서 삭제되는 경우 등)
            if(attackTarget== null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //target이 공격 범위 안에 있는지 검사(공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.transform.position, transform.position);

            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate);

            //공격(발사체 생성)
            SpawnProjectile();
        }

    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<ProjectTile>().Setup(attackTarget, attackDamage);
    }
}
