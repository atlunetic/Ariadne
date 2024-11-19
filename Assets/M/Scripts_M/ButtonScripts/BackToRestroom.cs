using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToRestroom : MonoBehaviour
{
    public void ToRestroom()
    {
        SceneManager.LoadScene("S2_3_0_RestRoom");
    }
}
