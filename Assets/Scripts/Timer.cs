using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public TextMeshProUGUI currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        //if(timerActive == true)
        //{
        //    currentTime = currentTime - Time.deltaTime;
        //}
        currentTime = currentTime - Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //currentTimeText.text = "TIME: " + currentTime.ToString() + "s";
        currentTimeText.text = "TIMER: " + time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    //public void StartTimer()
    //{
    //    timerActive = true;
    //}
    
    //public void StopTimer()
    //{
    //    timerActive = false;
    //}
}
