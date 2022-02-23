using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{

    bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public Text currentTimeText;
    public ManagerScene sm;

    public static Timer instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Timer multiple instance");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if(currentTime <= 0)
            {
                timerActive = false;
                Debug.Log("timer finished ! Its the end of the party");
                sm.setCurrentSceneIndex(SceneIndex.DeathScene);
                sm.LoadGame();
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Time before the arrival of the police force: " + time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void ResetTimer()
    {
        currentTime = startMinutes * 60;
    }

    public void StartTimer()
    {
        timerActive = true;
    }
}

