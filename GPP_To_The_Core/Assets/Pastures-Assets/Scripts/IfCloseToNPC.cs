using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class IfCloseToNPC : MonoBehaviour
{

    private Transform Player;
    public  static bool inRange1 = false;
    public Canvas text;
    public Camera main;
    public Camera chat;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
        text.gameObject.SetActive(false);
        chat.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) < 6)
        {
            inRange1 = true;
            text.gameObject.SetActive(true);
        }
        else
        {
            inRange1 = false;
            text.gameObject.SetActive(false);
        }

        if (NPC.chatOn)
        {
            text.gameObject.SetActive(false);
            main.gameObject.SetActive(false);
            chat.gameObject.SetActive(true);
            
        }
        else
        {
            main.gameObject.SetActive(true);
            chat.gameObject.SetActive(false); 
        }
       
    }
}
