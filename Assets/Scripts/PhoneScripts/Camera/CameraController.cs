using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField]
    private GameObject CameraRect;

    [SerializeField]
    private GameObject Phone;

    [SerializeField]
    private GameObject ShowScreenShot;

    [SerializeField]
    private GameObject ShowScreenShotBG;

    [SerializeField]
    private GameObject SaveOrNotPopUp;
    public string FolderPath;
    private string TotalPath;
    private byte[] PNGbuffer;
    void Awake(){
        if(instance==null)
            instance=this;

    #if UNITY_EDITOR
        FolderPath = $"{Application.dataPath}/ScreenShots/";
    #else
        FolderPath = $"{Application.persistentDataPath}/ScreenShots/";
    #endif

    }

    public void ActiveCamera(){
        CameraRect.SetActive(true);
        Phone.SetActive(false);
        // UI 다 끄기
    }

    public void TakeScreenShot(){
        GameObject.Find("Main Camera").GetComponent<TakePicture>().ScreenShot(CameraRect.GetComponent<RectTransform>());
        CameraRect.SetActive(false);
        Phone.SetActive(true);
        // UI 다 켜기 
    }

    public void SaveImmediate(){  // <TakePicture> 에서 호출

    }

    public void SaveTemporary(byte[] PNGbuffer){  // <TakePicture> 에서 호출
        
        this.PNGbuffer = PNGbuffer; 

        // Save or Not 띄우기
        ShowScreenShot.GetComponent<Image>().sprite = GalleryController.instance.MakeSprite(PNGbuffer);
        ShowScreenShotBG.SetActive(true);
        SaveOrNotPopUp.SetActive(true);
    }

    public void SaveScreenShot(){
        int num = PhoneController.instance.NumOfScreenShots++;

        if (Directory.Exists(FolderPath) == false){
            Directory.CreateDirectory(FolderPath);
        }

        string filename = "ScreenShot_" + num.ToString();
        TotalPath = string.Copy(FolderPath) + filename;
        File.WriteAllBytes(TotalPath, PNGbuffer);
            
        GalleryController.instance.PrintToGallery(filename);
        closePopUP();
    }

    public void DontSaveScreenShot(){
        PNGbuffer = null;
        closePopUP();
    }

    private void closePopUP(){
        SaveOrNotPopUp.SetActive(false);
        ShowScreenShotBG.SetActive(false);
        // UI 다시 활성화
    }
}
