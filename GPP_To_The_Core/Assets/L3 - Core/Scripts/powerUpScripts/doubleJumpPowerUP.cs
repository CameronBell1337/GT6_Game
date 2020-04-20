using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpPowerUP : MonoBehaviour
{
    public GameObject pickUpEffect;
    public GameObject uiTXTDJ;
    public GameObject particleEffect;
    void Start()
    {
        uiTXTDJ.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //pickUpItem(other);
            StartCoroutine(PickUpItem(other));
        }

    }
    

    IEnumerator PickUpItem(Collider player)
    {
        
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        ThirdPersonCharacterController doubleJumpBool = player.GetComponent<ThirdPersonCharacterController>();
        doubleJumpBool.doubleJumpEnabled = true;
        Destroy(particleEffect);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        uiTXTDJ.SetActive(true);

        yield return new WaitForSeconds(3);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Destroy(uiTXTDJ);
        Destroy(pickUpEffect);
        Destroy(gameObject);
    }
}
