using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHpSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints; //�������� �̵����
    private List<Enemy> enemyList; //���� �ʿ� �����ϴ� ��� ���� ����

    //���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ����.
    public List<Enemy> EnemyList => enemyList;

    // Start is called before the first frame update
    void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        
        // ������ �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab); // ��������Ʈ ����
            Enemy enemy = clone.GetComponent<Enemy>(); // ��� ������ ���� enemy������Ʈ

            enemy.Setup(this, wayPoints); // waypoint������ �Ű������� setup()�Լ� ȣ��
            enemyList.Add(enemy); // �����׿� ��� ������ �� ���� ����

            SpawnEnemyHpSlider(clone); //�� ü���� ��Ÿ���� Slider UI ���� �� ����

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestoryEnemy(Enemy enemy)
    {
        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //��������Ʈ ����
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemyHpSlider(Enemy enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        //Slider UI ������Ʈ�� Parent("Canvas" ������Ʈ)�� �ڽ����� ����
    }
}
