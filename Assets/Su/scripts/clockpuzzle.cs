using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockpuzzle : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField]
    private Transform MinuteHand, HourHand;

    [SerializeField]
    // ���߸� ��Ÿ���� �޽���
    private GameObject winText;

    private void Start()
    {
        winText.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� ������ ��
        {
            CheckIfCorrectTime();
        }
    }

    private void CheckIfCorrectTime()
    {
        float minuteAngle = NormalizeAngle(MinuteHand.eulerAngles.z);
        float hourAngle = NormalizeAngle(HourHand.eulerAngles.z);

        bool minuteCheck = Mathf.Abs(minuteAngle - 180) <= 10; // ��ħ�� 180���� ���� 10��
        bool hourCheck = Mathf.Abs(hourAngle - 15) <= 10; // ��ħ�� 345���� ���� 10��

        if (minuteCheck && hourCheck)
        {
            winText.SetActive(true);
        }
    }

    // ������ 0������ 360�� ������ ����ȭ
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0) angle += 360;
        return angle;
    }
}




