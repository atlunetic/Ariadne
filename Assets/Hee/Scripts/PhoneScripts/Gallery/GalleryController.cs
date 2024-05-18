using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GalleryController : MonoBehaviour
{
    public static GalleryController instance;
    private string Path;
    public GameObject GalleryContent;
    public Transform PictureTab;

    [SerializeField]
    private GameObject emptyImage;

    [SerializeField]
    private GameObject emptyPicture;

    [SerializeField]
    private GameObject[] originPictures;
    
    private Dictionary<string,GameObject> Pictures = new Dictionary<string, GameObject>();
    private List<GameObject> Clues = new List<GameObject>();

    void Awake(){
        if(instance==null){
            instance = this;
        }
    }
    void Start() 
    { 
        Path = CameraController.instance.FolderPath;

        for(int i=0; i < originPictures.Length; i++){
            Pictures.Add("origin"+ i.ToString(), originPictures[i]);
        }
    }
    public void PrintToGallery(string screenshotname){  // String으로 바꾸기
        
        string totalPath = string.Copy(Path) + screenshotname; // 파일 지정
        byte[] PNGbuffer = File.ReadAllBytes(totalPath);

        Sprite sprite = MakeSprite(PNGbuffer);

        // 갤러리에 이미지 생성, 픽쳐 탭에 픽쳐 생성 후 연결
        GameObject pictureSet = Instantiate(emptyPicture,PictureTab);
        pictureSet.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        pictureSet.name = screenshotname + "Pic";

        GameObject imageIcon = Instantiate(emptyImage,GalleryContent.transform);
        imageIcon.GetComponent<Image>().sprite = sprite;
        imageIcon.name = screenshotname + "Img";

        Pictures.Add(imageIcon.name,pictureSet);
        pictureSet.SetActive(false);
        imageIcon.transform.SetAsFirstSibling();

        if(screenshotname.Substring(0,10).Equals("ScreenShot")){
            // 프리팹으로 삭제버튼 만들어서 추가, 이름으로 찾기
        }
        else{
            Clues.Add(imageIcon);

        }
    }

    public void ActiveThePicture(string name){ 
        PhoneController.instance.ActiveTab(Pictures[name]); 
    }
    
    public Sprite MakeSprite(byte[] PNGbuffer){

        Texture2D imageTexture = new Texture2D(0, 0, TextureFormat.RGB24, false);
        imageTexture.LoadImage(PNGbuffer); // 텍스쳐 생성 후 이미지 로드

        Rect rect = new Rect(0, 0, imageTexture.width, imageTexture.height);
        return Sprite.Create(imageTexture, rect, Vector2.one * 0.5f); // 스프라이트 생성 후 반환
    }
}
