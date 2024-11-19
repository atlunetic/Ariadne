using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

public class MemoController : MonoBehaviour
{
    public static MemoController instance;
    void Awake(){ if(instance==null){ instance = this; } }

    public GameObject MemoScrollContent;
    public GameObject MemoPageContainer;
    public GameObject MemoShortPrefab;
    public Dictionary<string,GameObject> MemoPages  = new Dictionary<string, GameObject>();

    [HideInInspector]
    public GameObject NowOpenedPage;

    private Dictionary<string, string> MemoName = new Dictionary<string, string>(){
        {"memo_tornpage","찢긴 페이지"},
        {"memo_videotape","비디오 테이프"},
        {"memo_phone","휴대폰"},
        {"memo_letter","편지"},
        {"memo_recorder","녹음기"},
        {"memo_book","책"},
        {"memo_board","????"},
        {"memo_zzokji","쪽지"}
    };
    void Start()
    {
        int NumOfMemos = MemoName.Count;

        for(int i = 0; i < NumOfMemos; i++) 
        {
            GameObject memoPage = MemoPageContainer.transform.GetChild(i).gameObject;
            MemoPages.Add(memoPage.name, memoPage);
        }

        foreach(string memo in GameManager.instance.FindedMemoList)
        {
            AddMemo(memo);
        }
    }

    [YarnCommand("FindMemo")]
    public void FindMemo(string memo)
    {
        GameManager.instance.FindedMemoList.Add(memo);
        AddMemo(memo);
    }

    void AddMemo(string memo){
        GameObject Memoshort = Instantiate(MemoShortPrefab, MemoScrollContent.transform);
        Memoshort.name = memo;
        Memoshort.transform.GetChild(0).GetComponent<TMP_Text>().text = MemoName[memo];
    }

    public void GoNextMemo(){
        int pageidx = GameManager.instance.FindedMemoList.IndexOf(NowOpenedPage.name);
        if(pageidx == GameManager.instance.FindedMemoList.Count-1) pageidx=-1;
        
        NowOpenedPage = MemoPages[GameManager.instance.FindedMemoList[pageidx+1]];
        PhoneController.instance.Backward();
        PhoneController.instance.ActiveTab(NowOpenedPage);
    }

    public void GoPrevMemo(){
        int pageidx = GameManager.instance.FindedMemoList.IndexOf(NowOpenedPage.name);
        if(pageidx == 0) pageidx= GameManager.instance.FindedMemoList.Count+1;
        
        NowOpenedPage = MemoPages[GameManager.instance.FindedMemoList[pageidx-1]];
        PhoneController.instance.Backward();
        PhoneController.instance.ActiveTab(NowOpenedPage);
    }
}
    
