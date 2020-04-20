using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedBoostPowerUp : MonoBehaviour
{
    public GameObject pickUpEffect;
    public GameObject uiTXTDJ;
    public GameObject particleEffect;
    public float dissolveAmount;
    public float timer;
    public bool isTouching;

    MeshRenderer meshRenderer;
    void Start()
    {
        uiTXTDJ.SetActive(false);
        isTouching = false;
        dissolveAmount = 15f;
        
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
        meshRenderer.material.SetFloat("_Amount", dissolveAmount);
        meshRenderer.material.SetFloat("_Scale", timer);

        if(isTouching == true)
        {
            //timer = 0f;
            timer += (1f * Time.deltaTime);
            //timer = Mathf.Sin(Time.time * 1) * 3.0f;
            meshRenderer.material.SetFloat("_Scale", timer);
            
        }
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
        isTouching = true;
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        dissolveAmount = 2f;
        Destroy(particleEffect);
        //GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        uiTXTDJ.SetActive(true);

        yield return new WaitForSeconds(3);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Destroy(uiTXTDJ);
        Destroy(pickUpEffect);
        Destroy(gameObject);
    }
}
