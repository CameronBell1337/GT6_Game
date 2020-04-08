﻿using System.Collections;
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
        minecart.position = path.GetPointAtDistance(distanceTravelled);
        Vector3 rotation = path.GetRotationAtDistance(distanceTravelled).eulerAngles;
        rotation.z = 0;
        minecart.rotation = Quaternion.Euler(rotation);

        if (direction > 0)
        {
            if (Vector3.Distance(path.GetPointAtDistance(distanceTravelled), path.GetPointAtDistance(path.length - 0.5f)) < 0.5f)
            {
                active = false;
                direction *= -1;
            }
        }
        else
        {
            if (Vector3.Distance(path.GetPointAtDistance(distanceTravelled), path.GetPointAtDistance(0)) < 0.5f)
            {
                active = false;
                direction *= -1;
            }
        }
    }
}
