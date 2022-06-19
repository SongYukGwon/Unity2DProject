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
    [SerializeField]
    private PlayerHp playerHp;
    [SerializeField]
    private PlayerGold playerGold;
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

        //리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        //적오브젝트 삭제
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemyHpSlider(GameObject enemy)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        //Slider UI 오브젝트를 Parent("Canvas" 오브젝트)의 자식으로 설정
        //ui는 캔버스의 지역오브젝트로 설정되어 있어서 화면에 보인다.
        sliderClone.transform.SetParent(canvasTransform);
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI가 쫓아 다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
