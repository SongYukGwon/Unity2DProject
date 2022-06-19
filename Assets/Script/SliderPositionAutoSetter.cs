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
        //쫒아다님
        targetTransform = target;
        //RectTransform 컴포넌트 정보 얻어오기
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        //쫒아다니던게 사라지면 슬라이드도 사라짐
       if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

       //오브젝트 위치가 갱신된 이후에 Slider UI도 함께 위치로 설정
       //-> Update보다 늦게 실행되야 됨.
       //오브젝트의 월드 좌표를 기준으로 화면에서의 좌표값을 구함

       Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
       
        //화면 내에서 좌표 +distance만큼 떨어진 위치를 Slider Ui의 위치로 선정
       rectTransform.position = screenPosition + distance;
    }
}
