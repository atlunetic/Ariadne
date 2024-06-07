using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;
using Yarn.Unity;

public class SaveAndLoad : MonoBehaviour
{
    private string FolderPath;
    void Awake(){
    #if UNITY_EDITOR
        FolderPath = $"{Application.dataPath}/Saves/";
    #else
        FolderPath = $"{Application.persistentDataPath}/Saves/";
    #endif
    }
    public void Save(){
        if (Directory.Exists(FolderPath) == false){
            Directory.CreateDirectory(FolderPath);
        }

        GameManager.instance.items = Inventory.instance.items;

        SaveGameManager save = new SaveGameManager();
        save = GameManager.instance.Convert();
        string json = JsonConvert.SerializeObject(save, Formatting.Indented);

        string Totalpath = FolderPath+"Save";
        int i=0;
        // while (File.Exists(Totalpath + i.ToString())) i++;
        File.WriteAllText(Totalpath + i.ToString(), json);
        Debug.Log(Totalpath + i.ToString()+" 에 저장완료");
    }

    public void Load(string filename){

        DestroyImmediate(GameObject.Find("MenuUI"));
        string json = File.ReadAllText(FolderPath+filename);

        int num = GameManager.instance.NumOfScreenShots;

        SaveGameManager save = JsonConvert.DeserializeObject<SaveGameManager>(json);
        GameManager.instance.reConvert(save);
        GameManager.instance.IsLoad = true;
        GameManager.instance.NumOfScreenShots = num > GameManager.instance.NumOfScreenShots ? num : GameManager.instance.NumOfScreenShots;

        SceneManager.LoadScene("Menu");

        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue("RandomN");

        Debug.Log("저장된 씬: "+GameManager.instance.NowScene);
    }

    public void LoadSave0(){
        Load("Save0");
    }
}
