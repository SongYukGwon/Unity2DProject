using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; // 현재 스테이지의 모든 웨이브 정보
    [SerializeField]
    private Spawner enemySpawner;
    private int currentWaveIndex = -1; // 현재 웨이브 인덱스

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    public void StartWave()
    {
        //현재 맵에 적이없고, wave가 남아 있으면
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            //인덱스의 시작이 -1이기 떄문에 웨이브 인덱스 증가를 제일 먼저 함
            currentWaveIndex++;
            //EnemySpawner의 StartWave()함수 호출, 현재 웨이브 정보 제공
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

//구조체 클래스 직렬화
//다른 클래스에서 이 구조체를 사용할때 inspector view에서 클래스 내부 변수 정보 수정가능
[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}
