using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneClickHandler : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private bool isClickable;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        isClickable = false;
        UpdateColliderState();
    }

    void UpdateColliderState()
    {
        boxCollider.enabled = isClickable;
    }

    public void EnableClickability()
    {
        isClickable = true;
        UpdateColliderState();
    }

    void OnMouseDown()
    {
        if (isClickable)
        {
            Debug.Log("Phone clicked");
        }
    }
}
