using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClickWhileOpen : MonoBehaviour
{
    void Update()
    {
        if(!Menu.instance.BlockClick) Menu.instance.BlockClick = true;
    }
}
