using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; // ���� ���������� ��� ���̺� ����
    [SerializeField]
    private Spawner enemySpawner;
    private int currentWaveIndex = -1; // ���� ���̺� �ε���

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    public void StartWave()
    {
        //���� �ʿ� ���̾���, wave�� ���� ������
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            //�ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ���� ��
            currentWaveIndex++;
            //EnemySpawner�� StartWave()�Լ� ȣ��, ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

//����ü Ŭ���� ����ȭ
//�ٸ� Ŭ�������� �� ����ü�� ����Ҷ� inspector view���� Ŭ���� ���� ���� ���� ��������
[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}