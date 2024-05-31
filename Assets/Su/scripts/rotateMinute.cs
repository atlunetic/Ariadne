using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateMinute : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;
    private Collider2D col;

    public Transform hourHand; // ��ħ�� ���� ����
    private float lastMinuteAngle; // ���������� ����� ��ħ�� ����

    private void Start()
    {
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
        lastMinuteAngle = transform.eulerAngles.z; // ���� �� ��ħ�� ���� ����
    }

    private void Update()
    {
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                float clickAngle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                angleOffset = clickAngle - transform.eulerAngles.z;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                Vector3 vec3 = Input.mousePosition - screenPos;
                float currentMinuteAngle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                float adjustedMinuteAngle = currentMinuteAngle - angleOffset;

                // ������ 0������ 360�� ������ �����ϰ�, ���� ����� �ð� ��ġ�� "����"
                adjustedMinuteAngle = (adjustedMinuteAngle + 360) % 360;
                adjustedMinuteAngle = Mathf.Round(adjustedMinuteAngle / 30) * 30;  // 30�� �������� ����

                transform.eulerAngles = new Vector3(0, 0, adjustedMinuteAngle);

                // ��ħ ���� ������Ʈ
                float deltaAngle = adjustedMinuteAngle - lastMinuteAngle;
                if (deltaAngle < -180) deltaAngle += 360;
                if (deltaAngle > 180) deltaAngle -= 360;

                float hourAngleIncrement = deltaAngle / 12;
                float newHourAngle = hourHand.eulerAngles.z + hourAngleIncrement;

                hourHand.eulerAngles = new Vector3(0, 0, newHourAngle);
                lastMinuteAngle = adjustedMinuteAngle; // ���� ��ħ ������ ����
            }
        }
    }
}




