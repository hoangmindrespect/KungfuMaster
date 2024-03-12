using UnityEngine;
using System.Collections;

public class NPC : Interactable
{
    public string[] dialogue;
    public string name;

    public override void Interact()
    {
        //We don't have static class, static object but
        //we have static instance reference to the object that
        //the component is attached to.
        DialogueSystem.Instance.AddNewDialogue(dialogue, name);

        Debug.Log("Interacting with NPC.");
    }
}