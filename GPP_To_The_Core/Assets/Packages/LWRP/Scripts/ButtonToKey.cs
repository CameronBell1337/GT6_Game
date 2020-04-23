using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToKey : MonoBehaviour
{
    public KeyCode keyboardKey;
    public string axis;
    public bool axisPositive;

    bool axisRegistered = false;

    void Update()
    {
        if (Input.GetKeyDown(keyboardKey))
        {
            GetComponent<Button>().onClick.Invoke();
        }

        if (!axisRegistered && axisPositive && Input.GetAxis(axis) > 0.1f)
        {
            axisRegistered = true;
            GetComponent<Button>().onClick.Invoke();
        }

        if (!axisRegistered && !axisPositive && Input.GetAxis(axis) < -0.1f)
        {
            axisRegistered = true;
            GetComponent<Button>().onClick.Invoke();
        }

        if (Input.GetAxis(axis) > -0.1f && Input.GetAxis(axis) < 0.1f)
        {
            axisRegistered = false;
        }
    }
}
