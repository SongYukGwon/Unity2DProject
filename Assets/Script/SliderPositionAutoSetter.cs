using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 20.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        //�i�ƴٴ�
        targetTransform = target;
        //RectTransform ������Ʈ ���� ������
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        //�i�ƴٴϴ��� ������� �����̵嵵 �����
       if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

       //������Ʈ ��ġ�� ���ŵ� ���Ŀ� Slider UI�� �Բ� ��ġ�� ����
       //-> Update���� �ʰ� ����Ǿ� ��.
       //������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ���� ����

       Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
       
        //ȭ�� ������ ��ǥ +distance��ŭ ������ ��ġ�� Slider Ui�� ��ġ�� ����
       rectTransform.position = screenPosition + distance;
    }
}
