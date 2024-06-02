using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LockPick : MonoBehaviour
{
    public Camera cam;
    public Transform innerLock;
    public Transform pickPosition;

    public float maxAngle = 90;
    public float lockSpeed = 10;

    [Range(1, 25)]
    public float lockRange = 10;

    [SerializeField]
    private float timeLimit = 10f; // 게임의 시간 제한을 설정할 수 있는 필드

    private float eulerAngle;
    private float unlockAngle;
    private Vector2 unlockRange;

    private float keyPressTime = 0;
    private float timer = 0; // 타이머 추가

    private int attemptCount = 0; // 시도 횟수 추적
    private const int maxAttempts = 2; // 최대 시도 횟수

    private bool movePick = true;
    private bool isGameActive = true; // 게임 상태 확인

    // Start is called before the first frame update
    void Start()
    {
        newLock();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;

        transform.localPosition = pickPosition.position;

        if (movePick)
        {
            Vector3 dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);

            eulerAngle = Vector3.Angle(dir, Vector3.up);

            Vector3 cross = Vector3.Cross(Vector3.up, dir);
            if (cross.z < 0) { eulerAngle = -eulerAngle; }

            eulerAngle = Mathf.Clamp(eulerAngle, -maxAngle, maxAngle);

            Quaternion rotateTo = Quaternion.AngleAxis(eulerAngle, Vector3.forward);
            transform.rotation = rotateTo;
        }

        if (Input.GetMouseButtonDown(0))
        {
            movePick = false;
            keyPressTime = 1;
        }
        if (Input.GetMouseButtonUp(0))
        {
            movePick = true;
            keyPressTime = 0;
        }

        timer += Time.deltaTime; // 타이머 업데이트
        if (timer > timeLimit)
        {
            if (attemptCount >= maxAttempts)
            {
                //Debug.Log("You can't try it again");
                var runner = FindObjectOfType<DialogueRunner>();
                //runner.StartDialogue(game_openthedoor_gameover);
            }
            else
            {
                ++attemptCount;
                //Debug.Log("You lose!"); // 2번까지 실패했을때 대화로 진행&대화후 게임 다시 실행해야함
            }
        }

        float percentage = Mathf.Round(100 - Mathf.Abs(((eulerAngle - unlockAngle) / 100) * 100));
        float lockRotation = ((percentage / 100) * maxAngle) * keyPressTime;
        float maxRotation = (percentage / 100) * maxAngle;

        float lockLerp = Mathf.Lerp(innerLock.eulerAngles.z, lockRotation, Time.deltaTime * lockSpeed);
        innerLock.eulerAngles = new Vector3(0, 0, lockLerp);

        if (lockLerp >= maxRotation - 1)
        {
            if (eulerAngle < unlockRange.y && eulerAngle > unlockRange.x)
            {
                Debug.Log("Unlocked!");
                newLock();
                resetGame();
            }
            else
            {
                float randomRotation = Random.insideUnitCircle.x;
                transform.eulerAngles += new Vector3(0, 0, Random.Range(-randomRotation, randomRotation));
            }
        }
    }

    void newLock()
    {
        unlockAngle = Random.Range(-maxAngle + lockRange, maxAngle - lockRange);
        unlockRange = new Vector2(unlockAngle - lockRange, unlockAngle + lockRange);
        resetGame(); // 게임을 재시작 할 때마다 타이머를 리셋합니다.
    }

    void resetGame()
    {
        timer = 0; // 타이머 리셋
        isGameActive = true; // 게임 상태를 활성화합니다.
    }
}



