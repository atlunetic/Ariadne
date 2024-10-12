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
            Debug.LogError("clockPuzzle script not found in the scene.");  // GameOver 인스턴스가 없을 경우 에러 로깅
        }

        gameWin = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isGameActive) return;

        rotateminute.EnableDragging();
        var runner = FindObjectOfType<DialogueRunner>();
        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 놓았을 때
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

        bool minuteCheck = Mathf.Abs(minuteAngle - 180) <= 10; // 분침은 180도에 오차 10도
        bool hourCheck = Mathf.Abs(hourAngle - 15) <= 10; // 시침은 345도에 오차 10도

        if (minuteCheck && hourCheck)
        {
            gameWin = true;
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




