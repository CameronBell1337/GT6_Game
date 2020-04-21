using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatforms : MonoBehaviour
{
    public Vector3[] waypoints;

    public int currentWaypoint = 0;

    private Vector3 currentTargetWaypoint;

    public cameraManager platformCutscene;
    public BoxCollider triggerCol;
    public Animator Animator;

    public float smoothDamping;
    public float platformSpeed;
    public float delayTime;

    private float delayStart;

    public bool autoStart;

    Vector3 currentDest;


    void Start()
    {
        platformCutscene = FindObjectOfType<cameraManager>();

        
        
        if (waypoints.Length > 0)
        {
            currentTargetWaypoint = waypoints[0];
        }

        smoothDamping = platformSpeed * Time.deltaTime;
    }

    
    void FixedUpdate()
    {
        if (transform.position != currentTargetWaypoint)
        {
            MovePlatform();
        }
        else 
        {
            UpdateNextWaypoint();
        }
    }

     void MovePlatform()
    {
        currentDest = currentTargetWaypoint - transform.position;
        transform.position += (currentDest / currentDest.magnitude) * platformSpeed * Time.deltaTime;

        if(currentDest.magnitude < smoothDamping)
        {
            
            transform.position = currentTargetWaypoint;
            delayStart = Time.time;
        }
    }

    public void UpdateNextWaypoint()
    {
        if(autoStart)
        {
            if(Time.time - delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        currentWaypoint++;
        
        if (currentWaypoint >= waypoints.Length)
        {
            
            currentWaypoint = 0;
        }
        currentTargetWaypoint = waypoints[currentWaypoint];
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (currentDest.magnitude >= smoothDamping)
            {
                Animator.applyRootMotion = true;
            }
            else
            {
                Animator.applyRootMotion = false;
            }
            other.transform.parent = transform;
            //platformCutscene.isPlatformSceneActive = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Animator.applyRootMotion = false;
            other.transform.parent = null;
            //platformCutscene.isPlatformSceneActive = false;
            triggerCol.gameObject.SetActive(true);
        }
    }
}
