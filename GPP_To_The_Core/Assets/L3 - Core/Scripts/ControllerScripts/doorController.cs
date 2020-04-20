using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class doorController : MonoBehaviour
{
    public GameObject player;
    public GameObject doorBody;

    public bool doorIsOpening;
    public bool openDoor;
    public float doorAnimSpeed = 2.0f;
    public float currentYPos;
    public float currentTime;
    public float delay = 7.0f;

    public PlayableDirector timeline;

    

    void Start()
    {
        doorIsOpening = false;
        openDoor = false;
        currentYPos = doorBody.transform.position.y;
        currentTime = delay;
        timeline.GetComponent<PlayableDirector>();
        player.GetComponent<ThirdPersonCharacterController>();
    }

    void Update()
    {
        if (doorIsOpening)
        {
            currentTime -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (doorIsOpening)
        {
            timeline.Play();
            //StartCoroutine(PickUpItem());
            transform.Translate(Vector3.forward * Time.deltaTime * 0.1f);
            if (currentTime <= 0)
            {
                doorBody.transform.Translate(Vector3.up * Time.deltaTime * doorAnimSpeed);
                
            } 
        }
        if (doorBody.transform.position.y >= (currentYPos + 7.67f))
        {
            doorIsOpening = false;
            gameObject.SetActive(false);
            //openDoor = false;
            //timeline.Stop();

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<ThirdPersonCharacterController>().attacking == true && other.gameObject.tag == "Player")
        {
            doorIsOpening = true;
        }
    }
   
}
