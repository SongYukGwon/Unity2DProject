using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;


    private void Awake()
    {
        //MainCamera �±׸� ������ �ִ� ������Ʈ Ž�� �Ŀ� Camera ������Ʈ ���� ����
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //���콺 ���� ��ư�� ��������
        if(Input.GetMouseButtonDown(0))
        {
            //ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ� ���� ����
            //ray.origin ������ ������ġ
            //ray.direction ������ �������
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //2d ����͸� ���� 3d������ ������Ʈ�� ���콺�� �����ϴ� �پ���
            //������ �ε����� ������Ʈ�� �����ؼ� hit�� ����
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //������ �ε��� ������Ʈ�� �±װ� 'tile'�̸�
                if(hit.transform.CompareTag("Tile"))
                {
                    //Ÿ���� �����ϴ� SpawnTower()ȣ��
                    towerSpawner.SpawnTower(hit.transform);
                }
            }
        }
    }
}
