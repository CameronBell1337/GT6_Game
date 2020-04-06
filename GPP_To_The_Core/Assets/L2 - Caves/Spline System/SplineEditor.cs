using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SplineEditor : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numSegments;
    public GameObject waypoint;
    public GameObject waypointToggle;
    public GameObject[] waypointsAndToggles = new GameObject[0];

    [HideInInspector] public Vector3[] segmentPositions;
    private SphereCollider startTriggerVolume;
    private SphereCollider endTriggerVolume;
    // By serializing, the variable data is kept between editor mode and play mode, then hiding that
    // `with HideInInspector will hide this serialized variable in editor mode. The variable is still
    // `classed as serialized, even though it is hidden in the inspector, so the data is carried over
    // `between editor and play mode
    [HideInInspector] [SerializeField] private int numWaypointsAndToggles = 4;
    [HideInInspector] [SerializeField] private Vector3 lastSelectedWaypointLocalPos;
    [HideInInspector] [SerializeField] private Vector3 lastStaticSelectedWaypointLocalPos;
    [HideInInspector] [SerializeField] private Vector3 lastLinkedToggleBehindLocalPos;
    [HideInInspector] [SerializeField] private Vector3 lastLinkedToggleInfrontLocalPos;

    void Update()
    {
        if (Application.isEditor)
        {
            UpdateMirroredTogglePos();

            // Show editor visuals
        }
        else
        {
            // Hide editor visuals
        }

        DrawCurve();

        PositionTriggerVolumes();
    }

    /*private void LateUpdate()
    {
        if (Application.isEditor)
        {
            GetLateUpdateWaypointPositions();
        }
    }*/

    public void ResetSpline()
    {
        // Delete old waypoints/waypoint toggles
        foreach (GameObject gameObject in waypointsAndToggles)
        {
            DestroyImmediate(gameObject);
        }

        // Setup new default waypoints/waypoint toggles
        numWaypointsAndToggles = 4;
        waypointsAndToggles = new GameObject[numWaypointsAndToggles];

        waypointsAndToggles[0] = Instantiate(waypoint as GameObject);
        waypointsAndToggles[0].transform.SetParent(transform);
        waypointsAndToggles[0].transform.position = transform.position;

        waypointsAndToggles[1] = Instantiate(waypointToggle as GameObject);
        waypointsAndToggles[1].transform.SetParent(transform);
        waypointsAndToggles[1].transform.position = transform.position + (-Vector3.right * 0.5f) +
                                                    (Vector3.forward * 0.2f);

        waypointsAndToggles[2] = Instantiate(waypointToggle as GameObject);
        waypointsAndToggles[2].transform.SetParent(transform);
        waypointsAndToggles[2].transform.position = transform.position + (-Vector3.right * 0.5f) +
                                                    (Vector3.forward * 0.8f);

        waypointsAndToggles[3] = Instantiate(waypoint as GameObject);
        waypointsAndToggles[3].transform.SetParent(transform);
        waypointsAndToggles[3].transform.position = transform.position + Vector3.forward;

        Debug.Log("Reset");
    }

    public void AddWaypoint()
    {
        int preNumCurves = ((numWaypointsAndToggles - 1) / 3);

        // Increase array size by 3, whilst keeping array data
        numWaypointsAndToggles += 3;
        GameObject[] arrayCopy = new GameObject[numWaypointsAndToggles];
        for (int i = 0; i < waypointsAndToggles.Length; i++)
        {
            arrayCopy[i] = waypointsAndToggles[i];
        }
        waypointsAndToggles = new GameObject[numWaypointsAndToggles];
        for (int i = 0; i < arrayCopy.Length; i++)
        {
            waypointsAndToggles[i] = arrayCopy[i];
        }

        //Add new waypoints/waypoint toggles
        waypointsAndToggles[(preNumCurves * 3) + 1] = Instantiate(waypointToggle as GameObject);
        waypointsAndToggles[(preNumCurves * 3) + 1].transform.SetParent(transform);
        Vector3 dist1 = waypointsAndToggles[(preNumCurves * 3) - 1].transform.position -
                        waypointsAndToggles[preNumCurves * 3].transform.position;
        waypointsAndToggles[((preNumCurves * 3) + 1)].transform.position = waypointsAndToggles[preNumCurves * 3]
                                                                      .transform.position + -dist1;

        waypointsAndToggles[(preNumCurves * 3) + 2] = Instantiate(waypointToggle as GameObject);
        waypointsAndToggles[(preNumCurves * 3) + 2].transform.SetParent(transform);
        Vector3 dist2 = waypointsAndToggles[(preNumCurves * 3) - 2].transform.position -
                        waypointsAndToggles[(preNumCurves * 3) - 3].transform.position;
        Vector3 dist3 = waypointsAndToggles[preNumCurves * 3].transform.position -
                        waypointsAndToggles[(preNumCurves * 3) - 3].transform.position;
        waypointsAndToggles[((preNumCurves * 3) + 2)].transform.position = waypointsAndToggles[preNumCurves * 3]
                                                                      .transform.position + dist3 + -dist2;

        waypointsAndToggles[(preNumCurves * 3) + 3] = Instantiate(waypoint as GameObject);
        waypointsAndToggles[(preNumCurves * 3) + 3].transform.SetParent(transform);
        waypointsAndToggles[(preNumCurves * 3) + 3].transform.position = waypointsAndToggles[preNumCurves * 3]
                                                                      .transform.position + dist3;

        Debug.Log("Waypoint added");
    }

    private void UpdateMirroredTogglePos()
    {
        if (CheckForSelectedToggleIdx() != -1 && CheckForSelectedToggleIdx() % 3 != 0)
        {
            GameObject selectedToggle = waypointsAndToggles[CheckForSelectedToggleIdx()];
            GameObject linkedWaypoint;
            GameObject mirrorWaypointToggle;
            // Get linkedWaypoint & mirrorWayPointToggle & reposition based on mirrored position
            switch (CheckForSelectedToggleIdx() % 3)
            {
                case 1:
                    {
                        if (CheckForSelectedToggleIdx() - 2 > 0)
                        {
                            linkedWaypoint = waypointsAndToggles[CheckForSelectedToggleIdx() - 1];
                            mirrorWaypointToggle = waypointsAndToggles[CheckForSelectedToggleIdx() - 2];

                            Vector3 dist = selectedToggle.transform.localPosition -
                                           linkedWaypoint.transform.localPosition;
                            mirrorWaypointToggle.transform.localPosition = linkedWaypoint
                                                                           .transform.localPosition + -dist;
                        }
                        break;
                    }
                case 2:
                    {
                        if (CheckForSelectedToggleIdx() + 2 < waypointsAndToggles.Length)
                        {
                            linkedWaypoint = waypointsAndToggles[CheckForSelectedToggleIdx() + 1];
                            mirrorWaypointToggle = waypointsAndToggles[CheckForSelectedToggleIdx() + 2];

                            Vector3 dist = selectedToggle.transform.localPosition -
                                           linkedWaypoint.transform.localPosition;
                            mirrorWaypointToggle.transform.localPosition = linkedWaypoint
                                                                           .transform.localPosition + -dist;
                        }
                        break;
                    }
            }
        }
        /*else if (CheckForSelectedToggleIdx() % 3 == 0)
        {
            //waypointsAndToggles[CheckForSelectedToggleIdx()].transform.hasChanged = false;

            GameObject selectedWaypoint = waypointsAndToggles[CheckForSelectedToggleIdx()];
            GameObject linkedToggleBehind;
            GameObject linkedToggleInfront;

            if (lastSelectedWaypointLocalPos ==
                waypointsAndToggles[CheckForSelectedToggleIdx()].transform.localPosition)
            {
                waypointsAndToggles[CheckForSelectedToggleIdx()].transform.hasChanged = false;

                lastStaticSelectedWaypointLocalPos = waypointsAndToggles[CheckForSelectedToggleIdx()]
                                                     .transform.localPosition;

                Debug.Log("test");
                if (CheckForSelectedToggleIdx() - 1 > 0)
                {

                    linkedToggleBehind = waypointsAndToggles[CheckForSelectedToggleIdx() - 1];

                    linkedToggleBehind.transform.localPosition = selectedWaypoint.transform.localPosition +
                                                                 lastLinkedToggleBehindLocalPos;
                }
                if (CheckForSelectedToggleIdx() + 1 < waypointsAndToggles.Length)
                {

                    linkedToggleInfront = waypointsAndToggles[CheckForSelectedToggleIdx() + 1];

                    linkedToggleInfront.transform.localPosition = selectedWaypoint.transform.localPosition +
                                                                  lastLinkedToggleInfrontLocalPos;
                }
            }
        }
    }

    private void GetLateUpdateWaypointPositions()
    {
        if (CheckForSelectedToggleIdx() % 3 == 0)
        {
            if (waypointsAndToggles[CheckForSelectedToggleIdx()].transform.hasChanged)
            {
                lastSelectedWaypointLocalPos = waypointsAndToggles[CheckForSelectedToggleIdx()]
                                           .transform.localPosition;
            }
            else
            {
                if (CheckForSelectedToggleIdx() - 1 > 0)
                {
                    lastLinkedToggleBehindLocalPos = waypointsAndToggles[CheckForSelectedToggleIdx() - 1]
                                                     .transform.localPosition - lastSelectedWaypointLocalPos;
                }

                if (CheckForSelectedToggleIdx() + 1 < waypointsAndToggles.Length)
                {
                    lastLinkedToggleInfrontLocalPos = waypointsAndToggles[CheckForSelectedToggleIdx() + 1]
                                                       .transform.localPosition - lastSelectedWaypointLocalPos;
                }
            }
        }*/
    }

    private int CheckForSelectedToggleIdx()
    {
        for (int i = 0; i < waypointsAndToggles.Length; i++)
        {
            if (waypointsAndToggles[i] == Selection.activeGameObject)
            {
                return i;
            }
        }
        return -1;
    }

    private void DrawCurve()
    {
        int numCurves = (numWaypointsAndToggles - 1) / 3;

        segmentPositions = new Vector3[(numSegments * numCurves) + 1];
        segmentPositions[0] = waypointsAndToggles[0].transform.position;

        lineRenderer.positionCount = (numSegments * numCurves) + 1;

        for (int i = 0; i < numCurves; i++)
        {
            //Debug.Log("Curve:" + i);
            for (int j = 1; j < numSegments + 1; j++)
            {
                //Debug.Log("Segment in loop:" + j);
                float t = j / (float)numSegments;
                //Debug.Log("Current line segment:" + ((i * numSegments) + j));
                segmentPositions[(i * numSegments) + j] = CalculateCubicBezierPoint(t, waypointsAndToggles[0 + (i * 3)].transform.position,
                                                                waypointsAndToggles[1 + (i * 3)].transform.position,
                                                                waypointsAndToggles[2 + (i * 3)].transform.position,
                                                                waypointsAndToggles[3 + (i * 3)].transform.position);
            }
        }

        lineRenderer.SetPositions(segmentPositions);
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //Equation: result = (1-t)^3P0 + 3(1-t)^2tP1 + 3(1-t)t^2P2 + t^3P3

        float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;
        float tt = t * t;
        float ttt = tt * t;

        Vector3 point = uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;

        return point;
    }

    private void PositionTriggerVolumes()
    {
        startTriggerVolume = transform.Find("Start Trigger Volume").GetComponent<SphereCollider>();
        startTriggerVolume.transform.localPosition = waypointsAndToggles[0].transform.localPosition;

        endTriggerVolume = transform.Find("End Trigger Volume").GetComponent<SphereCollider>();
        endTriggerVolume.transform.localPosition = waypointsAndToggles[waypointsAndToggles.Length - 1]
                                                   .transform.localPosition;
    }
}
