using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int wayPointCount; //�̵� ��� ����
    private Transform[] wayPoints; // �̵� ��� ����
    private int currentIndex = 0; // ���� �̵���� �ε���
    private Movement movement2D; // ������Ʈ �̵�����
    private Spawner enemySpawner; //���� ������ ������ ���� �ʰ� EnemySpanwer�� �˷��� ����

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
            //ȸ���ϸ鼭 ��
            transform.Rotate(Vector3.forward * 10);

            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    // ���� ��ǥ�� �ٲٴ� �Լ�
    private void NextMoveTo()
    {
        //�̵��� way����Ʈ�� ���� �ִٸ����
        if (currentIndex < wayPoints.Length -1)
        {
            //������ ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;
            //���� ��ǥ��������
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //���� ��ġ�� ������ wayPoint�̸� ������Ʈ ����
        else
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        //EnemySpanwer���� ����Ʈ�� �� �������� -> �װ����� �� �ν��Ͻ��� �����ؾ���.(����Ʈ��������)
        enemySpawner.DestoryEnemy(this);
    }
}
