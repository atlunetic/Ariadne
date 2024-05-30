using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfo : MonoBehaviour
{
    public GameObject[] Objectlist;  // 상호작용 후 다음에 다시 씬에 들어왔을 때 비활성화 되어야 하는 오브젝트들
    public GameObject[] Cluelist;  // 카메라로 찍어야 하는 증거 오브젝트들

    void Awake(){
        SceneManager.sceneLoaded += SceneManage;
    } 

    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManage;
    }

    void SceneManage(Scene scene, LoadSceneMode mode){
        foreach(GameObject obj in Objectlist)
	        obj.SetActive(GameManager.instance.ObjectState[obj.name]);
    }
}
