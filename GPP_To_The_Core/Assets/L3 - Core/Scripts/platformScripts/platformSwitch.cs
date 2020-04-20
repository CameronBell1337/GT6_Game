using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class platformSwitch : MonoBehaviour
{
    public GameObject switchOBJ;
    public bool isSwitchPressed;
    public GameObject player;
    public movingPlatforms triggerPlatform;
    public ThirdPersonCharacterController thirdPersonCon;
    public PlayableDirector timeline1;
    public BoxCollider trigger;

    void Start()
    {
        thirdPersonCon = FindObjectOfType<ThirdPersonCharacterController>();
        isSwitchPressed = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isSwitchPressed)
        {
            timeline1.Play();
            StartCoroutine(AnimationDelay());
            triggerPlatform.autoStart = true;
            Destroy(trigger);
        }

        
    }

    void OnTriggerStay(Collider other)
    {
        if (player.GetComponent<ThirdPersonCharacterController>().attacking == true && other.gameObject.tag == "Player")
        {
                isSwitchPressed = true;
        }
    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(1);
        isSwitchPressed = false;
        
    }
}
