using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneInfo : MonoBehaviour
{
    public GameObject[] Objectlist;  // 상호작용 후 다음에 다시 씬에 들어왔을 때 비활성화 되어야 하는 오브젝트들
    public GameObject[] Cluelist;  // 카메라로 찍어야 하는 증거 오브젝트들
    public GameObject[] MemoriesBG;  // 기억 돌아올때 연출 배경
    public GameObject[] MemoriesImage;  // 기억 돌아올때 팝업이미지
    void Awake(){
        SceneManager.sceneLoaded += SceneManage;
    } 
    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManage;
    }

    void SceneManage(Scene scene, LoadSceneMode mode){
        foreach(GameObject obj in Objectlist)
	        obj.SetActive(!GameManager.instance.FindedObjects.Contains(obj.name));
        //GameManager.instance.NowScene = scene.name;
    }

    public string FindClue(Rect rect){
        if(Cluelist is null) return null;
        foreach(GameObject obj in Cluelist){
            if(GameManager.instance.FindedClues.Contains(obj.name)) continue;
            Vector3 pos = Camera.main.WorldToScreenPoint(obj.transform.position);
            if((rect.x + rect.width/5)<pos.x && (rect.x + rect.width/5 * 4)>pos.x){
                if((rect.y + rect.height/5)<pos.y && (rect.y + rect.height/5 * 4)>pos.y){
                    GameManager.instance.FindedClues.Add(obj.name);
                    StartCoroutine(PlayMemory(obj.name));
                    return obj.name;
                }
            }
        }
        return null;
    }

    GameObject memoryImg;
    IEnumerator PlayMemory(string cluename){
        var runner = FindObjectOfType<DialogueRunner>();

        foreach(GameObject BG in MemoriesBG){
            BG.SetActive(true);
            yield return new WaitForSeconds(0.6f);
        }
        
        foreach(GameObject BG in MemoriesBG){
            BG.SetActive(false);
        }
        MemoriesBG[3].SetActive(true);

        foreach(GameObject memoryImg in MemoriesImage)
            if(memoryImg.name == cluename+"Img")
                memoryImg.SetActive(true);

        //runner.StartDialogue(cluename);
    }

    [YarnCommand("StopMemory")]
    public void StopMemory(){  // 얀에서 부를 함수
        MemoriesBG[3].SetActive(false);
        memoryImg.SetActive(false);
        Menu.instance.UI_off();
    }
}
