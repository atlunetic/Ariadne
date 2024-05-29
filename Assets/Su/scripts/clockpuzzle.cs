using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockpuzzle : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField]
    private Transform MinuteHand, HourHand;

    [SerializeField]
    // 맞추면 나타나는 메시지
    private GameObject winText;

    private void Start()
    {
        winText.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 놓았을 때
        {
            CheckIfCorrectTime();
        }
    }

    private void CheckIfCorrectTime()
    {
        float minuteAngle = NormalizeAngle(MinuteHand.eulerAngles.z);
        float hourAngle = NormalizeAngle(HourHand.eulerAngles.z);

        bool minuteCheck = Mathf.Abs(minuteAngle - 180) <= 10; // 분침은 180도에 오차 10도
        bool hourCheck = Mathf.Abs(hourAngle - 15) <= 10; // 시침은 345도에 오차 10도

        if (minuteCheck && hourCheck)
        {
            winText.SetActive(true);
        }
    }

    // 각도를 0도에서 360도 범위로 정규화
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0) angle += 360;
        return angle;
    }
}




