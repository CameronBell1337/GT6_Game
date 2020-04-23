using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterUI : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = PlayerStats.coins.ToString();
    }
}
