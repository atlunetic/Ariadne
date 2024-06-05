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
    private float timeLimit = 10f; // ������ �ð� ������ ������ �� �ִ� �ʵ�

    private float eulerAngle;
    private float unlockAngle;
    private Vector2 unlockRange;

    private float keyPressTime = 0;
    private float timer = 0; // Ÿ�̸� �߰�

    private int attemptCount = 0; // �õ� Ƚ�� ����
    private const int maxAttempts = 2; // �ִ� �õ� Ƚ��

    public bool gameSuccess = false;
    private bool movePick = true;
    private bool isGameActive = true; // ���� ���� Ȯ��

    private GameOver gameOver;  // GameOver ��ũ��Ʈ ����

    public AudioClip[] hairpinSounds; // ���� ���� hairpin ���� ������ �����ϴ� �迭
    private AudioSource audioSource; // AudioSource ������Ʈ

    private DialogueRunner runner; // DialogueRunner ����

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ �߰�
        gameOver = FindObjectOfType<GameOver>();  // GameOver �ν��Ͻ� ã��
        runner = FindObjectOfType<DialogueRunner>(); // DialogueRunner �ν��Ͻ� ã��

        if (gameOver == null)
        {
            Debug.LogError("GameOver script not found in the scene.");  // GameOver �ν��Ͻ��� ���� ��� ���� �α�
        }

        if (runner == null)
        {
            Debug.LogError("DialogueRunner script not found in the scene.");  // DialogueRunner �ν��Ͻ��� ���� ��� ���� �α�
        }

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
            PlayRandomHairpinSound(); // ���콺 Ŭ�� �� ������ ���� ���
        }
        if (Input.GetMouseButtonUp(0))
        {
            movePick = true;
            keyPressTime = 0;
        }

        timer += Time.deltaTime; // Ÿ�̸� ������Ʈ
        if (timer > timeLimit)
        {
            if (gameSuccess == false)
            {
                if (attemptCount >= maxAttempts)
                {
                    Debug.Log("You can't try it again"); //�׽�Ʈ �� �̺κ� �����ϸ� ��
                    isGameActive = false;
                    movePick = false;
                    if (runner != null)
                    {
                        runner.StartDialogue("game_openthedoor_gameover");
                    }
                }
                else
                {
                    ++attemptCount;
                    Debug.Log(attemptCount);
                    isGameActive = false;
                    movePick = false;
                    Debug.Log("You lose");
                    if (runner != null)
                    {
                        runner.StartDialogue("game_retry");
                    }
                }
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
                Debug.Log("Unlocked!"); // �׽�Ʈ �� �����ص� ����
                gameSuccess = true;
                isGameActive = false;
                movePick = false;
                if (gameOver != null)
                {
                    gameOver.HideTimer();  // Ÿ�ӹٸ� ����
                }
                if (runner != null)
                {
                    runner.StartDialogue("game_openthedoor_success");
                }
            }
            else
            {
                float randomRotation = Random.insideUnitCircle.x;
                transform.eulerAngles += new Vector3(0, 0, Random.Range(-randomRotation, randomRotation));
            }
        }
    }

    [YarnCommand("NewLock")]
    public void newLock()
    {
        unlockAngle = Random.Range(-maxAngle + lockRange, maxAngle - lockRange);
        unlockRange = new Vector2(unlockAngle - lockRange, unlockAngle + lockRange);
        resetGame(); // ������ ����� �� ������ Ÿ�̸Ӹ� �����մϴ�.
        if (gameOver != null)
        {
            gameOver.RestartTimer();  // Ÿ�̸� ����
        }
    }

    void resetGame()
    {
        timer = 0; // Ÿ�̸� ����
        isGameActive = true; // ���� ���¸� Ȱ��ȭ�մϴ�.
    }

    void PlayRandomHairpinSound()
    {
        if (hairpinSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, hairpinSounds.Length);
            audioSource.PlayOneShot(hairpinSounds[randomIndex]);
        }
    }
}





