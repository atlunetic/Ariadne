using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneInfo : MonoBehaviour
{
    public GameObject[] Objectlist;  // 상호작용 후 다음에 다시 씬에 들어왔을 때 비활성화 되어야 하는 오브젝트들
    public GameObject[] Diarylist;
    public GameObject[] Cluelist;  // 카메라로 찍어야 하는 증거 오브젝트들
    public GameObject[] MemoriesBG;  // 기억 돌아올때 연출 배경

    public bool IsS4 = false;  // 씬 인스펙터 창에서 설정

    void Awake(){
        SceneManager.sceneLoaded += SceneManage;
    } 
    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManage;
    }

    void SceneManage(Scene scene, LoadSceneMode mode){
        foreach(GameObject obj in Objectlist)
	        obj.SetActive(!GameManager.instance.FindedObjects.Contains(obj.name));
        foreach(GameObject obj in Diarylist)
	        obj.SetActive(GameManager.instance.FindedObjects.Contains("Books_Diary"));
        GameManager.instance.NowScene = scene.name;
    }

    public string FindClue(Rect rect){
        if(Cluelist is null) return null;
        foreach(GameObject obj in Cluelist){
            if(GameManager.instance.FindedClues.Contains(obj.name)) continue;
            Vector3 pos = Camera.main.WorldToScreenPoint(obj.transform.position);
            if((rect.x + rect.width/5)<pos.x && (rect.x + rect.width/5 * 4)>pos.x){
                if((rect.y + rect.height/5)<pos.y && (rect.y + rect.height/5 * 4)>pos.y){
                    GameManager.instance.FindedClues.Add(obj.name);
                    print("Find "+obj.name);
                    StartCoroutine(PlayMemory(obj.name));
                    return obj.name;
                }
            }
        }
        return null;
    }

    GameObject memoryImg;
    public IEnumerator PlayMemory(string cluename){
        var runner = FindObjectOfType<DialogueRunner>();

        if(!IsS4){
            BGoff();
            for(int i=0; i<3; i++){
                MemoriesBG[i].SetActive(true);
                yield return new WaitForSeconds(0.6f);
            }
            MemoriesBG[3].SetActive(true);

            foreach(GameObject BG in MemoriesBG){
                BG.SetActive(false);
            }
        }
        GalleryController.instance.Glitch.SetActive(true);
        foreach(GameObject memoryimg in GalleryController.instance.MemoryImages)
            if(memoryimg.name == cluename+"Img"){
                Debug.Log(memoryimg.name);
                memoryimg.SetActive(true);
                memoryImg = memoryimg;
            }

        runner.StartDialogue("memory_"+cluename);
    }

    [YarnCommand("StopMemory")]
    public void StopMemory(){  // 얀에서 부를 함수
        
        memoryImg.SetActive(false);
        GalleryController.instance.Glitch.SetActive(false);

        if(IsS4) return;
        MemoriesBG[3].SetActive(false);
        Menu.instance.UI_off();
        BGon();
    }

    [YarnCommand("BGoff")]
    public void BGoff(){
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
    }

    [YarnCommand("BGon")]
    public void BGon(){
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
    }

    [YarnFunction("IsS4")]
    public static bool returnIsS4(){
        if(GameManager.instance.NowScene.StartsWith("S4")) return true;
        else return false;
    }
}
