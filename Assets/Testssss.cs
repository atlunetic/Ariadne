using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testssss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

       gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImage/Key");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
