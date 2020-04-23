using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounterUI : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = ((int)(PlayerStats.timer / 60)).ToString() + " minutes " + ((int)(PlayerStats.timer % 60)).ToString() + " seconds";
    }
}
