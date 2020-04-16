using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineFollowPath : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject player;
    public bool leftDir;
    public bool rightDir;

    public int waypointsIndex;
    void Start()
    {
        leftDir = false;
        rightDir = false;
        waypointsIndex = 0;
    }

    void nextWaypoint()
    {
        var lookDir = waypoints[waypointsIndex].transform.position - player.transform.position;
        lookDir.y = 0.0f;

        

        if (Vector3.Distance(waypoints[waypointsIndex].transform.position, player.transform.position) <= 1f)
        {
            if (leftDir)
            {
                waypointsIndex++;
                if (waypointsIndex > waypoints.Length)
                {
                    waypointsIndex = waypointsIndex + 1;
                }
            }

            if (rightDir)
            {
                waypointsIndex--;
                if (waypointsIndex > waypoints.Length)
                {
                    waypointsIndex = waypointsIndex - 1;
                }
            }
        }

        player.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //GetComponent<Rigidbody>().MovePosition(pos);
        //waypointsIndex = (waypointsIndex + 1) % waypoints.Length;
    }
    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<splineMovementController>().horizontal > 0)
        {
            leftDir = true;
            rightDir = false;
        }
        if (player.GetComponent<splineMovementController>().horizontal < 0)
        {
            leftDir = false;
            rightDir = true;
        }
        nextWaypoint();
    }
}
