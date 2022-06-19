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

        //Ÿ�� �Ǽ� ���� Ȯ��
        // ���� Ÿ����ġ�� get���� true�̸� Ÿ���� �ִ°�
        if (tile.IsBuildTower == true)
        {
            return;
        }

        // Ÿ���� �������� ������ Ÿ���Ǽ�
        tile.IsBuildTower = true;

        GameObject clone = Instantiate(towerPrefab, tileTransform.position , Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
}
