using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrackController : MonoBehaviour
{
    [Header("Track Spline")]
    public PathCreator pathCreator;
    public bool startMinecartAtBeginning = true;

    [Header("Track Player Colliders")]
    public GameObject[] colliders;

    [Header("Minecart")]
    public Transform minecart;
    public float minecartMoveSpeed = 15;

    [Header("Spline Start Junction")]
    public TrackController startSpline;
    [Range(-1, 1)]
    public int startSplineDirection = 1;

    [Header("Spline End Junction")]
    public TrackController endSpline;
    [Range(-1, 1)]
    public int endSplineDirection = 1;

    [HideInInspector]
    public VertexPath path;

    [HideInInspector]
    public int direction = 0;
    [HideInInspector]
    public float distanceTravelled = 0;

    [HideInInspector]
    public bool active = false;

    [HideInInspector]
    public int tiltDirection = 0;
    [HideInInspector]
    public Vector3 tiltPositionOffset = Vector3.zero;
    [HideInInspector]
    public Vector3 tiltRotationOffset = Vector3.zero;

    GameObject player;

    private void Awake()
    {
        if (pathCreator == null)
        {
            pathCreator = GetComponent<PathCreator>();
        }

        path = pathCreator.path;

        if (minecart != null)
        {
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

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (minecart != null && active)
        {
            distanceTravelled += Time.deltaTime * minecartMoveSpeed * direction;
            MoveMinecart();

            Vector3 rotation = Vector3.zero;
            rotation.y = direction < 0 ? 180 : 0;
            player.transform.localRotation = Quaternion.Euler(rotation);
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
                EnableColliders();
                if (endSpline != null)
                {
                    endSpline.direction = endSplineDirection;
                    endSpline.distanceTravelled = endSpline.path.GetClosestDistanceAlongPath(minecart.position);
                    endSpline.DisableColliders();
                    minecart.GetComponent<MinecartTrigger>().trackController = endSpline;
                    minecart.parent = endSpline.gameObject.transform;
                    endSpline.minecart = minecart;
                    endSpline.active = true;
                    minecart = null;
                    active = false;
                }
                else
                {
                    StopMinecart();
                }
            }
        }
        else
        {
            if (Vector3.Distance(path.GetPointAtDistance(distanceTravelled), path.GetPointAtDistance(0)) < 0.5f)
            {
                EnableColliders();
                if (startSpline != null)
                {
                    startSpline.direction = startSplineDirection;
                    startSpline.distanceTravelled = startSpline.path.GetClosestDistanceAlongPath(minecart.position);
                    endSpline.DisableColliders();
                    minecart.GetComponent<MinecartTrigger>().trackController = startSpline;
                    minecart.parent = startSpline.gameObject.transform;
                    startSpline.minecart = minecart;
                    startSpline.active = true;
                    minecart = null;
                    active = false;

                }
                else
                {
                    StopMinecart();
                }
            }
        }
    }

    public void DisableColliders()
    {
        foreach (GameObject obj in colliders)
        {
            obj.SetActive(false);
        }
    }

    public void EnableColliders()
    {
        foreach (GameObject obj in colliders)
        {
            obj.SetActive(true);
        }
    }
}
