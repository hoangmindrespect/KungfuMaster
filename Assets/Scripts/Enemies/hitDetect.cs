using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDetect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        print("Hit player");
    }
}
