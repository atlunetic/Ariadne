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
        Cursor.visible = false;
        Phone.SetActive(false);
        Menu.instance.UIMode.SetActive(false);
    }

    public void TakeScreenShot(){
        GetComponent<AudioSource>().Play();
        GameObject cam =GameObject.Find("Main Camera");
        cam.GetComponent<TakePicture>().ScreenShot(CameraRect.GetComponent<RectTransform>());
        CameraRect.SetActive(false);
        Cursor.visible = true;
    }

    public void SaveImmediate(string cluename, byte[] PNGbuffer){  // <TakePicture> 에서 호출, 증거사진일때
        if (Directory.Exists(FolderPath) == false){
            Directory.CreateDirectory(FolderPath);
        }
        TotalPath = string.Copy(FolderPath) + cluename;

        int i=0;
        while (File.Exists(TotalPath + i.ToString())) i++;
        File.WriteAllBytes(string.Copy(TotalPath) + i.ToString(), PNGbuffer);
            
        GalleryController.instance.PrintToGallery(cluename + i.ToString());
        GameManager.instance.PhotoList.Add(cluename + i.ToString());
    }

    public void SaveTemporary(byte[] PNGbuffer){  // <TakePicture> 에서 호출
        
        this.PNGbuffer = PNGbuffer; 

        Phone.SetActive(true);
        // Save or Not 띄우기
        ShowScreenShot.GetComponent<Image>().sprite = GalleryController.instance.MakeSprite(PNGbuffer);
        ShowScreenShotBG.SetActive(true);
        SaveOrNotPopUp.SetActive(true);
    }

    public void SaveScreenShot(){
        int i = GameManager.instance.NumOfScreenShots++;

        if (Directory.Exists(FolderPath) == false){
            Directory.CreateDirectory(FolderPath);
        }

        TotalPath = string.Copy(FolderPath) + "ScreenShot_";
        while (File.Exists(TotalPath + i.ToString())) i++;
        File.WriteAllBytes(TotalPath + i.ToString(), PNGbuffer);
            
        GalleryController.instance.PrintToGallery("ScreenShot_" + i.ToString());
        GameManager.instance.PhotoList.Add("ScreenShot_" + i.ToString());
        closePopUP();
    }

    public void DontSaveScreenShot(){
        PNGbuffer = null;
        closePopUP();
    }

    private void closePopUP(){
        SaveOrNotPopUp.SetActive(false);
        ShowScreenShotBG.SetActive(false);
        Menu.instance.UIMode.SetActive(true);
    }
}
