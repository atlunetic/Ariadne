using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class clockpuzzle : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField]
    private Transform MinuteHand, HourHand;

    private bool gameWin = false;
    private bool isGameActive = true;

    private rotateMinute rotateminute;

    private void Start()
    {
        rotateminute = FindObjectOfType<rotateMinute>();

        if (rotateminute == null)
        {
            Debug.LogError("clockPuzzle script not found in the scene.");  // GameOver �ν��Ͻ��� ���� ��� ���� �α�
        }

        gameWin = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isGameActive) return;

        rotateminute.EnableDragging();
        var runner = FindObjectOfType<DialogueRunner>();
        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� ������ ��
        {
            CheckIfCorrectTime();
        }

        if (gameWin)
        {   
            rotateminute.DisableDragging();
            isGameActive = false;
            runner.StartDialogue("game_clock_success");
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
            gameWin = true;
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




