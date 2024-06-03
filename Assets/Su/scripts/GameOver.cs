using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] float maxTime = 5f;
    float timeLeft;
    Image timerBar;

    private LockPick lockPick;  // LockPick ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        lockPick = FindObjectOfType<LockPick>();  // ������ LockPick �ν��Ͻ� ã��

        if (lockPick == null)
        {
            Debug.LogError("LockPick not found in the scene.");  // LockPick �ν��Ͻ��� ���� ��� ���� �α�
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lockPick != null && !lockPick.gameSuccess && timeLeft > 0)  // LockPick �ν��Ͻ��� �ְ�, ���� ������ �ƴϸ�, �ð��� �������� ��
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else if (timeLeft <= 0)
        {
            Time.timeScale = 0;  // �ð��� 0�� �Ǹ� ���� ����
        }
    }
}
