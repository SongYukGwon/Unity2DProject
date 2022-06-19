using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //[SerializeField]
    //private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHpSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    //[SerializeField]
    //private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints; //�������� �̵����
    [SerializeField]
    private PlayerHp playerHp;
    [SerializeField]
    private PlayerGold playerGold;
    private Wave currentWave; // ���� ���̺� ����
    private List<Enemy> enemyList; //���� �ʿ� �����ϴ� ��� ���� ����

    //���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ����.
    public List<Enemy> EnemyList => enemyList;

    // Start is called before the first frame update
    void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        
        // ������ �ڷ�ƾ �Լ� ȣ��
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        //�Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        //���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    private IEnumerator SpawnEnemy()
    {
        //���� ������ �� ����
        int spawnEnemyCount = 0;

        //������̺꿡�� �����Ǿ���ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ� �� ������Ʈ ����
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();  // ��� ������ ���� Enemy������Ʈ


            enemy.Setup(this, wayPoints); // waypoint������ �Ű������� setup()�Լ� ȣ��
            enemyList.Add(enemy); // �����׿� ��� ������ �� ���� ����

            SpawnEnemyHpSlider(clone); //�� ü���� ��Ÿ���� Slider UI ���� �� ����

            spawnEnemyCount++;

            //�� ���̺긶�� sapwnTime�� �ٸ��� �ֱ⶧���� ������̺��� �ð����� ���
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }

    public void DestoryEnemy(EnemyDestoryType type,Enemy enemy, int gold)
    {
        if (type == EnemyDestoryType.Arrive)
        {
            playerHp.TakeDamage(1);
        }
        else if (type == EnemyDestoryType.Kill)
        {
            playerGold.CurrentGold += gold;
        }

        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //��������Ʈ ����
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemyHpSlider(GameObject enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        //Slider UI ������Ʈ�� Parent("Canvas" ������Ʈ)�� �ڽ����� ����
        //ui�� ĵ������ ����������Ʈ�� �����Ǿ� �־ ȭ�鿡 ���δ�.
        sliderClone.transform.SetParent(canvasTransform);
        //���� �������� �ٲ� ũ�⸦ �ٽ� (1,1,1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI�� �Ѿ� �ٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
