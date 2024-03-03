using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    private bool hasInteracted;
    bool isEnemy;

    public virtual void MoveToInteraction()
    {
        isEnemy = gameObject.tag == "Enemy";
        hasInteracted = false;
    }

    //void Update()
    //{
    //    if (!hasInteracted)
    //    {
    //        if (!isEnemy)
    //            Interact();
    //        hasInteracted = true;
    //    }
    //}

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }
}