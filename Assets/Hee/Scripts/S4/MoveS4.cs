using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveS4 : MonoBehaviour
{
    public GameObject MoveS4Panel;
    void Awake() {
        SceneManager.sceneLoaded+= (Scene s, LoadSceneMode l) => {
            if(SceneManager.GetActiveScene().name=="S4_2_R_JisooRoom" || 
                SceneManager.GetActiveScene().name=="S4_3_R_JiwonRoom")
                gameObject.SetActive(true);
            else 
                gameObject.SetActive(false);
        }; 
    }
    void Start(){
        GetComponent<Button>().onClick.AddListener(()=>{MoveS4Panel.SetActive(!MoveS4Panel.activeSelf);});
    }

    public void GoJiwonRoom(){
        if(GameManager.instance.NowScene=="S4_3_R_JiwonRoom") return;
        SceneManager.LoadScene("S4_3_R_JiwonRoom");
        MoveS4Panel.SetActive(false);
    }
    public void GoJisooRoom(){
        if(GameManager.instance.NowScene=="S4_2_R_JisooRoom") return;
        SceneManager.LoadScene("S4_2_R_JisooRoom");
        MoveS4Panel.SetActive(false);
    }
}
