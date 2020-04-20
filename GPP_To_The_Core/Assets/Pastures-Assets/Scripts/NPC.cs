using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    public bool inRange = false;
     private bool inChat = false;
     private bool inDialogue = true;
     private bool inDialogueLeftSubTree = false;
     private bool inDialogueUpSubTree = false;
     public static bool chatOn = false; 
     
     [Header("Objects")]

     public GameObject npcWindow;

     public Text chatText;

     public Text leftText;
     public Text upText;
     public Text rightText;

     [Header(" All possible Dialogue Options")]
     public string greeting;

     [Header("Dialogue1")] 
     public string left1;
     public string leftResponse1;
     public string up1;
     public string up1Response1;
     public string right1;
     public string rightResponse1;
     [Header("Dialogue1 LEFT Sub Tree")] 
     public string left2;
     public string leftResponse2;
     public string up2;
     public string up1Response2;
     public string right2;
     public string rightResponse2;
     [Header("Dialogue1 UP Sub Tree")] 
     public string left3;
     public string leftResponse3;
     public string up3;
     public string up1Response3;
     public string right3;
     public string rightResponse3;
     
    void Start()
    {
      //  inRange = true;
        npcWindow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IfCloseToNPC.inRange1)
             {
                 inRange = true;
             }
             else
             {
                 inRange = false;
             }
        
        if (Input.GetKeyDown("e"))
        {
            if (inRange && !inChat)
            {
                npcWindow.gameObject.SetActive(true);
                chatText.GetComponent<Text>().text = greeting;
                loadDialogue1();
                chatOn = true;
            }
            else
            {
                chatOn = false;
            }
        }

        
        
    }

    void loadDialogue1()
    {
        inChat = true;
        inDialogue = true;
        inDialogueLeftSubTree = false;
        inDialogueUpSubTree = false;
        leftText.GetComponent<Text>().text = left1;
        upText.GetComponent<Text>().text = up1;
        rightText.GetComponent<Text>().text = right1;
    }

    void loadDialogueLeftSubTree()
    {
        inDialogue = false;
        inDialogueLeftSubTree = true;
        inDialogueUpSubTree = false;
        leftText.GetComponent<Text>().text = left2;
        upText.GetComponent<Text>().text = up2;
        rightText.GetComponent<Text>().text = right2;
    }
    
    void loadDialogueLeftSubTree2()
    {
        inDialogue = false;
        inDialogueLeftSubTree = false;
        inDialogueUpSubTree = false;
        leftText.GetComponent<Text>().text = "";
        upText.GetComponent<Text>().text = "";
    }
    
    void loadDialogueUpSubTree()
    {
        inDialogue = false;
        inDialogueLeftSubTree = false;
        inDialogueUpSubTree = true;
        leftText.GetComponent<Text>().text = left3;
        upText.GetComponent<Text>().text = up3;
        rightText.GetComponent<Text>().text = right3;
    }
    
    void loadDialogueUpSubTree2()
    {
        inDialogue = false;
        inDialogueLeftSubTree = false;
        inDialogueUpSubTree = false;
        leftText.GetComponent<Text>().text = "";
        upText.GetComponent<Text>().text = "";
    }

    public void Left()
    {
        if (inDialogue)
        {
            chatText.GetComponent<Text>().text = leftResponse1;
            loadDialogueLeftSubTree();
        }else if (inDialogueLeftSubTree)
        {
            chatText.GetComponent<Text>().text = leftResponse2;
            loadDialogueLeftSubTree2();
        }else if (inDialogueUpSubTree)
        {
            chatText.GetComponent<Text>().text = leftResponse3;
            loadDialogueUpSubTree2();
        }
    }

    public void Up()
    {
        if (inDialogue)
        {
            chatText.GetComponent<Text>().text = up1Response1;
            loadDialogueUpSubTree();
        }else if (inDialogueLeftSubTree)
        {
            chatText.GetComponent<Text>().text = up1Response2;
            loadDialogueLeftSubTree2();
        }else if (inDialogueUpSubTree)
        {
            chatText.GetComponent<Text>().text = up1Response3;
            loadDialogueUpSubTree2();
        }
    }

    public void Right()
    {
        CloseDialogue();
    }

    void CloseDialogue()
    {
        npcWindow.gameObject.SetActive(false);
        inChat = false;
        chatOn = false;
    }
}
