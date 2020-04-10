using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrackController : MonoBehaviour
{
    public PathCreator pathCreator;
    public Transform minecart;

    public bool startMinecartAtBeginning;
    public float minecartMoveSpeed;

    VertexPath path;

    public bool active = false;

    [HideInInspector]
    public int direction = 0;
    float distanceTravelled = 0;

    public Vector3 tiltPositionOffset = Vector3.zero;
    public Vector3 tiltRotationOffset = Vector3.zero;

    private void Awake()
    {
        path = pathCreator.path;

        if (startMinecartAtBeginning)
        {
            distanceTravelled = 0;
            direction = 1;
        }
        else
        {
            distanceTravelled = path.length - 0.5f;
            direction = -1;
        }

        MoveMinecart();
    }

    private void Update()
    {
        if (active)
        {
            distanceTravelled += Time.deltaTime * minecartMoveSpeed * direction;
            MoveMinecart();
        }
    }

    private void StopMinecart()
    {
        active = false;
        direction *= -1;
        tiltPositionOffset = Vector3.zero;
        tiltRotationOffset = Vector3.zero;

        minecart.position = path.GetPointAtDistance(distanceTravelled);
        Vector3 rotation = path.GetRotationAtDistance(distanceTravelled).eulerAngles;
        rotation.z = 0;
        minecart.rotation = Quaternion.Euler(rotation);
    }

    private void MoveMinecart()
    {
        minecart.position = path.GetPointAtDistance(distanceTravelled) + tiltPositionOffset;
        Vector3 rotation = path.GetRotationAtDistance(distanceTravelled).eulerAngles;
        rotation.z = tiltRotationOffset.z;
        minecart.rotation = Quaternion.Euler(rotation);

        if (direction > 0)
        {
            if (Vector3.Distance(path.GetPointAtDistance(distanceTravelled), path.GetPointAtDistance(path.length - 0.5f)) < 0.5f)
            {
                StopMinecart();
            }
        }
        else
        {
            if (Vector3.Distance(path.GetPointAtDistance(distanceTravelled), path.GetPointAtDistance(0)) < 0.5f)
            {
                StopMinecart();
            }
        }
    }
}
