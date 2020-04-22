﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private float currentTime = 0;
    private float startingTime = 45;
    public Text countdownText;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        countdownText.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerUpCollect.isMagnet)
        {
            countdownText.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");  
            
        }else if(currentTime <= 0)
        {
            StartCoroutine(Stop());
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(5);
        countdownText.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
