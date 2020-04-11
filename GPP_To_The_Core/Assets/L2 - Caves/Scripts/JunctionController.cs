using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionController : MonoBehaviour
{
    public TrackController mainSpline;
    public TrackController splitSpline;

    [Range(-1, 1)]
    public int mainMovementDirection;
    [Range(1, -1)]
    public int leanDirection;
    [Range(-1, 1)]
    public int splitMovementDirection;

    bool inRange = false;

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    private void Update()
    {
        if (inRange && 
            mainSpline.minecart != null && 
            mainSpline.direction == mainMovementDirection && 
            mainSpline.tiltDirection == leanDirection)
        {
            splitSpline.direction = splitMovementDirection;
            splitSpline.distanceTravelled = splitSpline.path.GetClosestDistanceAlongPath(mainSpline.minecart.position);
            mainSpline.minecart.GetComponent<MinecartTrigger>().trackController = splitSpline;
            mainSpline.minecart.parent = splitSpline.gameObject.transform;
            splitSpline.minecart = mainSpline.minecart;
            splitSpline.active = true;
            mainSpline.minecart = null;
            mainSpline.active = false;
        }
    }


}
