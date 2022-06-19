using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private int towerBuildGold = 50;
    [SerializeField]
    private Spawner enemySpawner;
    [SerializeField]
    private PlayerGold playerGold;

    public void SpawnTower(Transform tileTransform)
    {
        if (towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        //Ÿ�� �Ǽ� ���� Ȯ��
        // ���� Ÿ����ġ�� get���� true�̸� Ÿ���� �ִ°�
        if (tile.IsBuildTower == true)
        {
            return;
        }

        // Ÿ���� �������� ������ Ÿ���Ǽ�
        tile.IsBuildTower = true;

        playerGold.CurrentGold -= towerBuildGold;

        int randomTowerIndex = Random.Range(0, towerPrefab.Length);

        GameObject clone = Instantiate(towerPrefab[randomTowerIndex], tileTransform.position , Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
}
