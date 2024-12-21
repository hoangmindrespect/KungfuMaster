using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuStart");
    }
}
