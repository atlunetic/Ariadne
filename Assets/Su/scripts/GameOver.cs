using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    float maxTime = 5f;
    float timeLeft;
    Image timerBar;

    private LockPick lockPick;  // LockPick 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        lockPick = FindObjectOfType<LockPick>();  // 씬에서 LockPick 인스턴스 찾기

        if (lockPick == null)
        {
            Debug.LogError("LockPick not found in the scene.");  // LockPick 인스턴스가 없을 경우 에러 로깅
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lockPick != null && !lockPick.gameSuccess && timeLeft > 0)  // LockPick 인스턴스가 있고, 게임 성공이 아니며, 시간이 남아있을 때
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
    }

    // Restart the timer
    public void RestartTimer()
    {
        timeLeft = maxTime;
        timerBar.fillAmount = timeLeft / maxTime;
    }

    public void HideTimer()
    {
        timerBar.gameObject.SetActive(false);  // 타임바를 비활성화
    }
}

