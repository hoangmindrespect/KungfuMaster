using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneFlagHandler : MonoBehaviour
{
    public int type;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == 0)
            {
                SceneManager.LoadScene("Boss1");
            }
            else if (type == 1)
            {
                SceneManager.LoadScene("Scene3");
            }
            else if (type == 2)
            {
                SceneManager.LoadScene("Boss2");
            }
        }
    }
}
