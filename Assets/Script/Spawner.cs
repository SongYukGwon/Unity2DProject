using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHpSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField]
    private Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints; //스테이지 이동경로
    private List<Enemy> enemyList; //현재 맵에 존재하는 모든 적의 정보

    //적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요없다.
    public List<Enemy> EnemyList => enemyList;

    // Start is called before the first frame update
    void Awake()
    {
        //적 리스트 메모리 할당
        enemyList = new List<Enemy>();
        
        // 적생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab); // 적오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 enemy컴포넌트

            enemy.Setup(this, wayPoints); // waypoint정보를 매개변수로 setup()함수 호출
            enemyList.Add(enemy); // 리스테에 방금 생성된 적 정보 저장

            SpawnEnemyHpSlider(clone); //적 체력을 나타내는 Slider UI 생성 및 설정

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestoryEnemy(Enemy enemy)
    {
        //리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        //적오브젝트 삭제
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemyHpSlider(Enemy enemy)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        //Slider UI 오브젝트를 Parent("Canvas" 오브젝트)의 자식으로 설정
    }
}
