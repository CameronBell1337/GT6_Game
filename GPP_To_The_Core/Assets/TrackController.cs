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
    public EndOfPathInstruction endInstruction = EndOfPathInstruction.Stop;

    public bool active = false;
    public bool playerInRange = false;

    int direction = 0;
    float distanceTravelled = 0;

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

    private void MoveMinecart()
    {
        minecart.position = path.GetPointAtDistance(distanceTravelled, endInstruction);
        Vector3 rotation = path.GetRotationAtDistance(distanceTravelled).eulerAngles;
        rotation.z = 0;
        minecart.rotation = Quaternion.Euler(rotation);
    }
}
