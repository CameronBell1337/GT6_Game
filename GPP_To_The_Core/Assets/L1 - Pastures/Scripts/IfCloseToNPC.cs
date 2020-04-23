using UnityEngine;

public class IfCloseToNPC : MonoBehaviour
{
    public bool inRange = false;
    public Canvas text;
    public Camera main;
    public Camera chat;
    public NPC NPCScript;

    private Transform player;
    private PlayerAction playerAction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerAction = player.GetComponent<PlayerAction>();
        text.gameObject.SetActive(false);
        chat.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= 6)
        {
            inRange = true;
            text.gameObject.SetActive(true);
            playerAction.canAttack = false;
        }
        else if (inRange && Vector3.Distance(transform.position, player.position) > 6)
        {
            inRange = false;
            text.gameObject.SetActive(false);
            playerAction.canAttack = true;
        }

        if (NPCScript.chatOn)
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
