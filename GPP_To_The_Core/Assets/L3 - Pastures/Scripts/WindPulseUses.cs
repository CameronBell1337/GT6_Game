using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindPulseUses : MonoBehaviour
{
    public Text uses;

    private WindPulseEffect effector;

    void Start()
    {
        effector = GameObject.FindGameObjectWithTag("Player").GetComponent<WindPulseEffect>();
    }

    void Update()
    {
        uses.text = effector.counting.ToString();
    }
}
