using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDetect : MonoBehaviour
{
    public Animator animator;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if(animator.GetBool("isAttack")){
        //     print("Minotaur hit player");
        // }
    }
}
