using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Spawner enemySpawner;

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        //타워 건설 여부 확인
        // 현재 타일위치에 get으로 true이면 타워가 있는것
        if (tile.IsBuildTower == true)
        {
            return;
        }

        // 타워가 있음으로 설정후 타워건설
        tile.IsBuildTower = true;

        GameObject clone = Instantiate(towerPrefab, tileTransform.position , Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
}
