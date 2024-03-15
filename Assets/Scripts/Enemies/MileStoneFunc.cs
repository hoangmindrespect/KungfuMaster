using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MileStoneFunc : MonoBehaviour
{
    private CrowDeathMovement cm;
    void OnTriggerEnter2D(Collider2D other)
    {
        cm = other.GetComponent<CrowDeathMovement>();
        cm.Flip();
    }
}
