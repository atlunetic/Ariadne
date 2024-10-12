using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class StartDialogueSpecial : MonoBehaviour
{

    public string RequiredItemName;
    public string CorrectItemDialogue;
    public string NoItemDialogue;
    public string WrongItemDialogue;
    


    private void OnMouseDown()
    {

        Debug.Log("Object is clicked");

        if (Menu.instance.BlockClick) return;

        var runner = FindObjectOfType<DialogueRunner>();
        item currentItem = Inventory.instance.GetUsingItem();

        if (currentItem != null && currentItem.itemName == RequiredItemName)
        {
            Debug.Log("Requirement met by using: " + currentItem.itemName);
            runner.StartDialogue(CorrectItemDialogue);
            Inventory.instance.ClearUsingItem(); // Clear the current item after use
        }
        else if (currentItem!=null && currentItem.itemName != RequiredItemName){
            runner.StartDialogue(WrongItemDialogue);
        }

        else
        {
            runner.StartDialogue(NoItemDialogue);
        }
        

    }
}
