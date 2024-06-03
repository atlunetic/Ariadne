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

        else if (currentItem != null)
        {
            runner.StartDialogue(NoItemDialogue);
        }
        

    }
}
