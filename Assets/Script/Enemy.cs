using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int wayPointCount; //이동 경로 개수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 이동경로 인덱스
    private Movement movement2D; // 오브젝트 이동제어
    private Spawner enemySpawner; //적의 삭제를 본인이 하지 않고 EnemySpanwer에 알려서 삭제

    // Start is called before the first frame update
    public void Setup(Spawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement>();
        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    // Update is called once per frame
    private IEnumerator OnMove()
    {
        NextMoveTo();

        while(true)
        {
            //회전하면서 감
            transform.Rotate(Vector3.forward * 10);

            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    // 다음 목표로 바꾸는 함수
    private void NextMoveTo()
    {
        //이동할 way포인트가 남아 있다면실행
        if (currentIndex < wayPoints.Length -1)
        {
            //적으이 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            //다음 목표지점으로
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //현재 위치가 마지막 wayPoint이면 오브젝트 삭제
        else
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        //EnemySpanwer에서 리스트로 적 정보관리 -> 그곳에서 이 인스턴스를 삭제해야함.(리스트관리위함)
        enemySpawner.DestoryEnemy(this);
    }
}
