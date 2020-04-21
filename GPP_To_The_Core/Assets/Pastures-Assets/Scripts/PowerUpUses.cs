using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUses : MonoBehaviour
{
    public Text uses;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uses.text = PowerUpEffect.counting.ToString();
    }
}
